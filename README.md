# NeighborConnecter
Choose a tile. Keeping your mouse pressed, hover over similar and neighbor tiles. When you release your mouse, if 3 or more tiles are selected, they will all be destroyed.
İstediğiniz gibi kullanabilirsiniz. Herhangi bir tavsiye, uyarı veya bug için 13yedecim13@gmail.com

# Game_Manager
Oyunu başlatan ve boardın büyüklüğüne karar veren scriptir.

boardSize : Boardın büyüklüğünü belirlediğimiz değişkendir.

# Canvas_Manager
Kaç puan aldığımızı gösterir.

textScore : Puanlarımızın gösterildiği text componenti değişkenidir.

# Board_Manager
Oyun sağasını oluşturan, tileları seçen, denetleyen ve düzgün seçilmişler ise onları yok edip tekrar oluşturan scripttir.

shrinkingBoard : Board oluşturulup tilelar seçilip yok edildikten sonra board küçülsün ve sütünda tile kalmadığı taktirde sola doğru kaysın mı 
yoksa yok olan tilaların üstündeki tilelar aşağı insin ve kalan boşluğa yeni tilelar oluşturulup yerleştirilsinmi durumunu belirlemek için kullanılır.

boardTilePrefab : Boardı oluşturmak için kullandığımız tile objesinin prefabıdır.

boardTileParent : Tileları bir arada tutabilmek için kullandığımız parent objesidir.

boardItemList : Boardda gösterilecek iconların belirlenmesi için kullanılan itemlerin tutulduğu listedir.

# Item
Boardda kullanılacak resimlerin ve puanlarının tutulduğu scriptable objedir.

Item : Puan ve icon değişkenlerinin tutulduğu scripttir.

point : Kaç puan olduğunu belirler

icon : Resmin neye benzediğini belirler.

Item_Editor : İconun, item objesinde değişkenin yanında texture'unda gösterilmesini sağlar.

# Tile
Board'ı görselleştirmek için kullanılır.

Tile : Boardı oluşturan kullanılan alt scripttir.

myCoordinate : Tile objesinin koordinatıdır.

myItem : Tile objesine atanmış itemdir. Kullanılacak icon ve alınacak puanı tutar.

mySpriteIcon : Tile üzerindeki spriterenderer componentidir. iconun gösterilmesine yarar.

myNeighbors : Tile objesinin komşularıdır.
