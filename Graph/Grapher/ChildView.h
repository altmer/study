// ChildView.h : interface of the CChildView class
//


#pragma once
#include "Grapher.h"
#include "Function.h"
#define PIX 1000
// CChildView window

class CChildView : public CWnd
{
// Construction
public:
	CChildView();

// Attributes
public:

// Operations
public:

// Overrides
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// Implementation
public:
	virtual ~CChildView();

	// Generated message map functions
protected:
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
protected:
	// graphic
	CPen m_axisPen;
	CPen m_gridPen;
	int m_Scale;
	bool b_Grid;

public:
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
public:
	void SetScale (int newScale);
	void SetGrid (bool newGrid);
	bool GetGrid ();
	int GetScale();
	int GetNormScale();
protected:	
	void DrawGraphic(CPaintDC* dc, CFunction& fun, COLORREF col);
public:
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
public:
	afx_msg void OnSize(UINT nType, int cx, int cy);
public:
	afx_msg void OnWindowPosChanged(WINDOWPOS* lpwndpos);
public:
	afx_msg void OnPreferences();
public:
	afx_msg void OnRenew();
public:
	afx_msg void OnMove(int x, int y);
};

