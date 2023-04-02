#pragma once
#include "stdafx.h"
#include "Function.h"
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

class CGrapher
{
private:
	vector <CFunction> baseFun;
	vector <COLORREF> colorFun;
	vector <bool> drawFun;
protected:
public:
	CGrapher(void);
	virtual ~CGrapher(void);
	int size();
	CFunction& getGraphic(int nID);
	COLORREF getColor(int nID);
	bool addGraphic (const string& s);
	bool getDraw(int ID);
	void deleteGraphic (int nID);
	void setColor (COLORREF newColor, int ID);
	void setDraw(bool newDraw, int ID);
	void clear();
};
