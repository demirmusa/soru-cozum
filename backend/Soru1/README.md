**Soru: 1.** Bize öyle bi method / fonksiyon hazırlayın ki, almış olduğu iki yazı (string) parametresini ölçüp birinin herhangi bir permutasyonunun diğerinin içinde olup olmadığının mantıksal (boolean) karşılığını bize dönsün.
    Örnek
    “baba” , “abab” => true
    “baba”, “abc” => false
    “lds”, “loodos” => true



**Çözüm açıklaması:**

Bir string değerinin diğer bir stringin herhangi bir kombinasyonunu içerebilmesi için şu şarta uyması gerekmektedir

* Kaynak string aranılan stringde bulunan tüm karakterleri en az aranılan stringdeki sayısı kadar içermelidir.

Örneğin:

​	String 1: **abab**, 

​	String 2: **cba**

​	**c** karakteri ilk dizide olmadığı için ilk string ikinci stringin herhangi bir kombinasyonunu içeremez.

Örnek 2:

​	String 1: **ababc**

​	String 2: **ccba** 

​	İkinci dizide 2 adet bulunan **c** karakteri ilk dizide yalnızca bir adet olduğundan, ilk string ikinci stringin herhangi bir kombinasyonunu içeremez.

Örnek 2:

​	String 1: **ababccc**

​	String 2: **ccba** 

​	String 1 string 2 ye ait en az bir kombinasyonu içerir.



Sorunun çözümüne bu şekilde yaklaşırsak. Verilen stringleri karakterlerine göre gruplayıp ardından ardından kaynak grubun aranılan gruba ait tüm karakterleri en az aranılan gruptaki karakter sayıları kadar içermesi kontrol edilecektir.

# Hiyerarşi
**Soru1:** İmplementasyonun bulunduğu proje  
**Soru1.Tests:** İlgili testlerin bulunduğu proje  
**Soru1.ConsoleApp:** Deneme yapabileceğiniz console uygulaması
