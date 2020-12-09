**Soru: 1.** Tek bir dizi içerisinde, 3 tane stack tutmak isterseniz, bunu nasıl yaparsınız?



**Çözüm açıklaması:**

3 tane stack olacağı için bir dizinin içerisinde mod3 e göre sıralanabilirler.

Örneğin:

Stack1: 1, 2, 3, 4

Stack2: 'a', 'b'

Stack3: "x", "y", "z"

Dizi üzerinde aşağıdaki gibi gözükecektir:

| Index: |  0   |  1   |  2   |  3   |  4   |  5   |  6   |  7   |  8   |  9   |  10  |  11  |
| ------ | :--: | :--: | :--: | :--: | :--: | :--: | :--: | :--: | :--: | :--: | :--: | :--: |
| Dizi   |  1   | 'a'  | "x"  |  2   | 'b'  | "y"  |  3   | null | "z"  |  4   | null | null |

*Not: 4 elemana kadar bu yöntem ile kullanılabilir. 5. elemana geldiğimizde 2 ile 4 ün ebobu 1 dışında bir sayı(2) olduğundan bu mantık çakışmalara neden olacaktır.*