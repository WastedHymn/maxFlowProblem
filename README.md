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


- **Kırmızı** renkli kenarlar akışın sıfır olması için kesilmesi gereken kenarları temsil ediyor.
- **Turuncu** renkli kenarlar akışın sıfırdan farklı olduğu kenarları temsil ediyor.

## Kaynaklar
1. https://www.youtube.com/watch?v=LdOnanfc5TM
2. https://www.youtube.com/watch?v=1ewLrXUz4kk
3. https://www.youtube.com/watch?v=M4fyCfFTYV8
4. https://www.youtube.com/watch?v=3LG-My_MoWc
5. https://www.youtube.com/watch?v=Iwc3Uj4aaF4
6. https://github.com/microsoft/automatic-graph-layout
7. https://www.youtube.com/watch?v=9d6QfNXGShM
8. https://www.youtube.com/watch?v=JP5rgXO_5Sk
9. https://brilliant.org/wiki/max-flow-min-cut-algorithm/
