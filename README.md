# NeighborConnecter
Show it and it will be connected to its neighbor. İstediğiniz gibi kullanabilirsiniz. Herhangi bir tavsiye, uyarı veya bug için 13yedecim13@gmail.com

# Game_Manager
Oyunu başlatan ve boardın büyüklüğüne karar veren scriptir.
boardSize : Boardın büyüklüğünü belirlediğimiz değişkendir.

# Canvas_Manager
Kaç puan aldığımızı gösterir.
textScore : Puanlarımızın gösterildiği text componenti değişkenidir.

# Board_Manager
Oyun sağasını oluşturan, tileları seçen, denetleyen ve düzgün seçilmişler ise onları yok edip tekrar oluşturan scripttir.
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
