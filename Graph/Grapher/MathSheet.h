#pragma once

#include "IntegrPage.h"
#include "Roots.h"

// CMathSheet

class CMathSheet : public CPropertySheet
{
	DECLARE_DYNAMIC(CMathSheet)

public:
	IntegrPage page1;
	Roots page2;
public:
	CMathSheet(UINT nIDCaption, CWnd* pParentWnd = NULL, UINT iSelectPage = 0);
	CMathSheet(LPCTSTR pszCaption, CWnd* pParentWnd = NULL, UINT iSelectPage = 0);
	virtual ~CMathSheet();

protected:
	DECLARE_MESSAGE_MAP()
};


