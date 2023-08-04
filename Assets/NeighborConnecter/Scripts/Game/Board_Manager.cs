using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public enum TileMouseType
{
	Down = 0,
	Enter = 1,
	Up = 2
}
public class Board_Manager : MonoBehaviour
{
    private static Board_Manager instance;
	public static Board_Manager Instance { get => instance; }

	private const float DoTweenDuration = 0.25f;
	[SerializeField] private Tile boardTilePrefab;
    [SerializeField] private Transform boardTileParent;
    [SerializeField] private List<Item> boardItemList = new List<Item>();

	private int score;
	private int scoreAdd = 0;
    private int width;
	private int height;
	private int totalSwapTiles = 0;
	private bool waitForChoosing;
	private Tile choosedTile;
	private List<Tile> choosedTiles = new List<Tile>();

	private Tile[,] myTile;
	private Dictionary<int, List<Tile>> swapTiles = new Dictionary<int, List<Tile>>();
	public Tile[,] Tiles { get => myTile; }
	public int Width { get => width; }
	public int Height { get => height; }
	public int ScoreAdd { get => scoreAdd; }
	public int Score { get => score; set => score = value; }

	[ContextMenu("Set Board")]
	private void SetBoard()
	{
        for (int e = boardTileParent.childCount - 1; e >= 0 ; e--)
        {
			Destroy(boardTileParent.GetChild(e).gameObject);
        }
		SetBoard(Game_Manager.Instance.BoardSize);
	}
	private void Awake()
	{
		if (instance == null)
        {
			instance = this;
		}
        else
        {
			Destroy(gameObject);
        }
    }

    /// <summary>
    /// Create a tile board
    /// </summary>
    public void SetBoard(Vector2Int boardSize)
	{
		width = boardSize.x;
		height = boardSize.y;
		myTile = new Tile[width, height];
		choosedTiles.Clear();
		ClearSwapTilesList();

		for (int h = 0; h < Width; h++)
		{
			for (int e = 0; e < Height; e++)
			{
				var tile = Instantiate(boardTilePrefab, new Vector3(h, e, 0), Quaternion.identity, boardTileParent);
				tile.name = "Tile : " + h + " --- " + e;
				myTile[h, e] = tile;
				Item item = boardItemList[Random.Range(0, boardItemList.Count)];
				tile.SetMyTile(item);
				tile.SetMyCoordinate(new Vector2Int(h, e));
			}
		}
        foreach (var tile in myTile)
        {

			tile.SetMyNeighbors();
		}
	}
	private void ClearSwapTilesList()
	{
		swapTiles.Clear();
		for (int e = 0; e < width; e++)
		{
			int swapOrder = e;
			swapTiles.Add(swapOrder, new List<Tile>());
		}
	}
	public void ChooseTile(Tile tile, TileMouseType tileMouseType)
	{
		if (waitForChoosing)
		{
			return;
		}
		if (tileMouseType == TileMouseType.Down) // Hold on mouse and choose first tile.
		{
			choosedTiles.Clear();
			choosedTile = tile;
			choosedTiles.Add(choosedTile);
		}
		else if (tileMouseType == TileMouseType.Enter)
		{
			// Not have choose tile so return.
			if (choosedTile == null)
			{
				return;
			}
			// Entered tile not equal choosed tile so return
			if (tile.MyItem != choosedTile.MyItem)
			{
				return;
			}
			// Entered tile and choosed tile are equal
			// Entered tile was chosen before. So we have to delete the next tiles.
			if (choosedTiles.Contains(tile))
            {
				bool findedTile = false;
				for (int e = choosedTiles.Count - 1; e > 0 && !findedTile; e--)
				{
					if (choosedTiles[e] != tile)
					{
						choosedTiles.RemoveAt(e);
					}
                    else
                    {
						findedTile = true;
					}
				}
				return;
			}
			// Tile not connected and it is neighbor the last tile. So connect this tile.
			if (choosedTiles[^1].IsMyNeighbors(tile))
			{
				choosedTiles.Add(tile);
			}
		}
		else if (tileMouseType == TileMouseType.Up)
		{
			// Not have choose tile so return.
			if (choosedTile == null)
			{
				return;
			}
			// Connecting is complete so check the tiles for destroying
			DestroyConnectedTiles();
		}
	}
	/// <summary>
	/// Connecting is complete so check the tiles for destroying
	/// </summary>
	private void DestroyConnectedTiles()
	{
		waitForChoosing = true;
		if (choosedTiles.Count > 2)
		{
			for (int h = 0; h < choosedTiles.Count; h++)
			{
				scoreAdd += choosedTiles[h].MyItem.point;
				myTile[choosedTiles[h].MyCoordinate.x, choosedTiles[h].MyCoordinate.y] = null;
				Destroy(choosedTiles[h].gameObject);
			}
			Canvas_Manager.Instance.SetScore();
			GoDownTile();
		}
		else
		{
			ClearChoosedTile();
		}
	}

	/// <summary>
	/// Slide down the tiles on top of the destroyed tiles. 
	/// </summary>
	private void GoDownTile()
	{
		ClearSwapTilesList();
		for (int e = 0; e < Width; e++)
		{
			for (int c = 0; c < Height; c++)
			{
				if (myTile[e, c] == null)
				{
					totalSwapTiles++;
					var tile = Instantiate(boardTilePrefab, new Vector3(e, Height + swapTiles[e].Count, 0), Quaternion.identity, boardTileParent);

					Item item = boardItemList[Random.Range(0, boardItemList.Count)];
					tile.SetMyTile(item);

					swapTiles[e].Add(tile);
					tile.gameObject.SetActive(false);
				}
				else
				{
					if (swapTiles[e].Count > 0)
					{
						SendAnotherCoordinateToTile(e, c, new Vector2Int(e, c - swapTiles[e].Count));
					}
				}
			}
		}
		CreateNewTiles();
	}
	/// <summary>
	/// Create new tiles and slide them into the null part.
	/// </summary>
	private void CreateNewTiles()
	{
		DOTween.To(value => { },
			startValue: 0, endValue: 1, duration: DoTweenDuration * 2)
			.OnComplete(() =>
			{
				for (int e = 0; e < Width; e++)
				{
					int swapTilesCount = swapTiles[e].Count;
					for (int c = 0; c < swapTilesCount; c++)
					{
						swapTiles[e][c].name = "Tile : " + e + " --- " + c;
						Vector2Int newCoordinate = new Vector2Int(e, Height - swapTiles[e].Count + c);
						myTile[e, newCoordinate.y] = swapTiles[e][c];
						swapTiles[e][c].SetMyCoordinate(newCoordinate);
						swapTiles[e][c].gameObject.SetActive(true);
						Vector3 newPos = new Vector3(newCoordinate.x, newCoordinate.y, 0);
						swapTiles[e][c].transform.DOMove(newPos, DoTweenDuration).OnComplete(() =>
						{
							totalSwapTiles--;
							if (totalSwapTiles == 0)
							{
								LearnNeighbors();
								ClearChoosedTile();
							}
						});
					}
				}
			});
	}
	private Tile SendAnotherCoordinateToTile(int oldX, int oldY, Vector2Int newCoordinate)
	{
		Tile tile = myTile[oldX, oldY];
		myTile[oldX, oldY] = null;
		myTile[newCoordinate.x, newCoordinate.y] = tile;
		tile.SetMyCoordinate(newCoordinate);
		Vector3 newPos = new Vector3(newCoordinate.x, newCoordinate.y, 0);
		tile.transform.DOMove(newPos, DoTweenDuration);

		return tile;
	}
	private void ClearChoosedTile()
	{
		waitForChoosing = false;
		choosedTile = null;
		choosedTiles.Clear();
	}
	private void LearnNeighbors()
	{
		for (int h = 0; h < Width; h++)
		{
			for (int e = 0; e < Height; e++)
			{
				myTile[h, e].SetMyNeighbors();
			}
		}
	}
	public void SetScore()
    {
		score += scoreAdd;
		scoreAdd = 0;
	}
}