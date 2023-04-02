#pragma once
#include <cstring>
#include <string>
#include <vector>
#include <cmath>
#include <bitset>
#include <functional>
#include <queue>
#include <stack>
#include <algorithm>
#include <numeric>
#include <map>
#include <set>
using namespace std;
#define POW -100
#define PLUS -3
#define MINUS -4
#define MULT -1
#define DIV -2
#define UNMINUS -5
#define COS -6
#define SIN -7
#define TAN -8
#define CTG -9
#define EXP -10
#define SQR -11
#define VAR -20

// Achtung!
// 1. Добавить поддержку отрицательной степени!
// 2. Добавить поддержку возведения выражений в степень!

class CFunction
{
private:
	vector <int> func;
	string view;
protected:
	class cmp 
	{
	public:
		bool operator()(const int& a, const int& b){
			return a>b;
		}
	};
	int StrToInt(const string& s);
	int WTF (string s);
	int WTO (char c);
	vector <int> split (string s);
public:
	CFunction();
	virtual ~CFunction();
	const char* GetString();
	bool Construct(string s);
	double operator () (double x);
};
