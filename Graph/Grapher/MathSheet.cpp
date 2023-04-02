// MathSheet.cpp : implementation file
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "MathSheet.h"


// CMathSheet

IMPLEMENT_DYNAMIC(CMathSheet, CPropertySheet)

CMathSheet::CMathSheet(UINT nIDCaption, CWnd* pParentWnd, UINT iSelectPage)
	:CPropertySheet(nIDCaption, pParentWnd, iSelectPage)
{

}

CMathSheet::CMathSheet(LPCTSTR pszCaption, CWnd* pParentWnd, UINT iSelectPage)
	:CPropertySheet(pszCaption, pParentWnd, iSelectPage)
{
	AddPage(&page1);
	page1.par = pParentWnd;
	AddPage(&page2);
	page2.par = pParentWnd;
}

CMathSheet::~CMathSheet()
{
}


BEGIN_MESSAGE_MAP(CMathSheet, CPropertySheet)
END_MESSAGE_MAP()


// CMathSheet message handlers
