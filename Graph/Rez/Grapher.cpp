#include "StdAfx.h"
#include "Grapher.h"
using namespace std;
CGrapher::CGrapher(void)
{
}

CGrapher::~CGrapher(void)
{
}

bool CGrapher::addGraphic(const string& s)
{
	CFunction tmp;
	if (!tmp.Construct(s))
		return false;
	baseFun.push_back(tmp);
	colorFun.push_back(RGB(rand()%256,rand()%256,rand()%256));
	drawFun.push_back(true);
	return true;
}

void CGrapher::deleteGraphic(int nID)
{
	baseFun.erase(baseFun.begin()+nID);
	colorFun.erase(colorFun.begin()+nID);
	drawFun.erase(drawFun.begin()+nID);
}

int CGrapher::size()
{
	return static_cast<int>(baseFun.size());
}

CFunction& CGrapher::getGraphic(int nID)
{
	return baseFun[nID];
}

COLORREF CGrapher::getColor(int nID)
{
	return colorFun[nID];
}

bool CGrapher::getDraw(int ID)
{
	return drawFun[ID];
}

void CGrapher::setColor(COLORREF newColor, int ID)
{
	colorFun[ID]=newColor;
}

void CGrapher::setDraw(bool newDraw, int ID)
{
	drawFun[ID]=newDraw;
}