#include <iostream>
#include <string>

// mallocをstatic_cast：キャストの練習
auto func1 = [](int size) -> int
{
    ////auto a = static_cast<char*>(std::malloc(sizeof(char) * size));
    ////auto b = new char[sizeof(char) * size];
    //auto b = static_cast<char*>(::operator new(sizeof(char) * size));
    //auto a = reinterpret_cast<short*>(b);


    //int i = 0;

    //b[i++] = 1;
    //*(b + i++) = 0;
    //b[i++] = 2;
    //b[i++] = 0;

    //i = 0;
    //std::cout << std::to_string(a[i++]) << std::endl;
    //std::cout << std::to_string(a[i++]) << std::endl;

    //::operator delete(b);

    auto c = std::make_unique<int[]>(size);
    //auto c = new int{123};
    auto d = new int[3]{ 1,2,3 };
    
    c[2] = 1;
    
    std::cout << c[2] << std::endl;


    //delete c;
    delete[] d;

    return 0;
};

struct ABC
{
    int x = 0;
    int y = 0;
    int z = 0;

    ABC() : x(0), y(0), z(0) {}
    ABC(int x, int y, int z) : x(x), y(y), z(z) {}
};

// スマートポインタ：変数
auto SafePtrFunc = []() -> void
{
    auto p = std::make_unique<int>(99);                     // 初期値が99

    *p = 111;

    std::cout << *p << std::endl;
};

// スマートポインタ：構造体
auto SafePtrFunc1 = []() -> void
{
    auto p = std::make_unique<ABC>(1,2,3);                  // 構造体の引数3つのコンストラクタを使って初期化

    p->x = 111;

    std::cout << p->x << std::endl;
};

// スマートポインタ：配列
auto SafePtrFunc2 = []() -> void
{
    auto p = std::make_unique<int[]>(3);                    // 配列の数が3つ
    //auto p = std::make_unique<int[]>(1,2,3);              // 初期化はできない
    //auto p = std::unique_ptr<int[]>(new int[3]{ 1,2,3 }); // 配列を初期化するとしたらこれ

    p[0] = 111;
    //*p = 0;       // こうは書けない

    std::cout << p[0] << std::endl;
};

// -----------------------------------------------------------------

// 生ポインタ：変数
auto UnsafePtrFunc = []() -> void
{
    //auto v = new int(123);
    auto v = new int{123};

    *v = 111;

    std::cout << *v << std::endl;

    //delete v;
};

// 生ポインタ：配列
auto UnsafePtrFunc2 = []() -> void
{
    auto v = new int[]{ 1, 2, 3 };  // ポインタであるv自体はstackにあるが、newした配列の実体はヒープにメモリ確保してる
    int p[]{ 1,2,3 };               // ローカル変数はstackに入る

    *(v + 0) = 11;
    *(v + 1) = 12;
    *(v + 2) = 13;


    std::cout << *v << std::endl;

    delete [] v;
};

// 生ポインタ：配列
auto UnsafePtrFunc3 = []() -> void
{
    auto v = new ABC(1,2,3);

    v->x = 11;

    std::cout << v->x << std::endl;

    delete[] v;
};




int main()
{
    for (int i = 0 ; i < 100000; i++)
    {
        SafePtrFunc2();
        UnsafePtrFunc2();
    }

    system("pause");
}


// newした構造体の初期値をいれたいときのやりかた
//https://brain.cc.kogakuin.ac.jp/~kanamaru/lecture/C++2/09/09-02.html
//samp** p; //  ポインタへのポインタ
//
//p = new samp * [10] // samp 型のポインタの配列 10 個を確保
//
//p[0] = new samp(0, 0);
//p[1] = new samp(1, 1);
//…
//
//// 使用する際は p[i]->get_product() のようにアクセス
//
//for (int i = 0; i < 10; i++) {  // 各ポインタの指すオブジェクトを削除した後…
//    delete p[i];
//}
//
//delete[] p;	  // ポインタへのポインタを削除