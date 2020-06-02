# Maksimum Akış Problemi

## Projenin Amacı
Sınırsız bir kaynaktan sınırsız bir havuza belirtilen tesisat üzerinden en fazla kaç birim akış sağlanabileceğini(Max-Flow) ve en fazla akış durumunda tesisattaki akışı sıfıra indirmek için en az kaç kenarın kesilmesi(Min-Cut) gerektiğini bulmak.

## Kullanılan Algoritmalar
Problemi çözmek için genel olarak kullanılan algoritma Ford-Fulkerson algoritmasıdır. Akış yolu bulmak için ise Capacity Scaling algoritması kullanımıştır.

## Ekran Görüntüleri

###### Örnek Graf
![graf1](screenshots/exampleGraph.jpg)

###### Graf Oluşturma Ekranı - 1
![menu1](screenshots/mainmenu3.jpg)

###### Graf Oluşturma Ekranı - 2
![menu2](screenshots/mainmenu4.jpg)

###### Graf Oluşturma Ekranı - 3
![menu2](screenshots/mainmenu2.jpg)

###### Çözüm Grafı
![graf2](screenshots/solutionGraph.jpg)

- <p style="color:red">Kırmızı</p> renkli kenarlar akışın sıfır olması için kesilmesi gereken kenarları temsil ediyor.
- Turuncu renkli kenarlar akışın sıfırdan farklı olduğu kenarları temsil ediyor.


