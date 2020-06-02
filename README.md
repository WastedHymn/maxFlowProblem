# Maksimum Akış Problemi

## Projenin Amacı
Sınırsız bir kaynaktan sınırsız bir havuza belirtilen tesisat üzerinden en fazla kaç birim akış sağlanabileceğini(Max-Flow) ve en fazla akış durumunda tesisattaki akışı sıfıra indirmek için en az kaç kenarın kesilmesi(Min-Cut) gerektiğini bulmak.

## Kullanılan Algoritmalar
Problemi çözmek için genel olarak kullanılan algoritma Ford-Fulkerson algoritmasıdır. Akış yolu bulmak için ise Capacity Scaling algoritması kullanımıştır.

## Proje Dökümanı
[Döküman](https://www.dropbox.com/s/xshgnfwkc6i3odg/YAZ%20LAB_II_3.proje.pdf?dl=0)
> Proje Visual Studio 2019 üzerinde C# dilinde yazıldı.

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
1. [Max Flow Ford Fulkerson | Network Flow | Graph Theory](https://www.youtube.com/watch?v=LdOnanfc5TM)
2. [Capacity Scaling | Network Flow | Graph Theory](https://www.youtube.com/watch?v=1ewLrXUz4kk)
3. [Max Flow Problem](https://www.youtube.com/watch?v=M4fyCfFTYV8)
4. [Ford Fulkerson algorithm for Maximum Flow Problem Example](https://www.youtube.com/watch?v=3LG-My_MoWc)
5. [Ford Fulkerson Algorithm for Maximum Flow Problem](https://www.youtube.com/watch?v=Iwc3Uj4aaF4)
6. [Microsoft Automatic Graph Layout](https://github.com/microsoft/automatic-graph-layout)
7. [How to create a message box in Windows Form Application](https://www.youtube.com/watch?v=9d6QfNXGShM)
8. [Modern Flat UI, Drop-down/Slider Menu, Side Menu, Responsive, Only Form - C#, WinForm](https://www.youtube.com/watch?v=JP5rgXO_5Sk)
9. [Max-flow Min-cut Algorithm](https://brilliant.org/wiki/max-flow-min-cut-algorithm/)
