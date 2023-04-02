// MainFrm.h : interface of the CMainFrame class
//


#pragma once

#include "ChildView.h"
#include "ToolDialog.h"
#include "MathSheet.h"
#include <exception>
class CMainFrame : public CFrameWnd
{
protected:
	static const char s_Heading[];	
	static const char s_Scale[];
	static const char s_Grid[];
	static const char s_File[];
public:
	CMainFrame();
protected: 
	DECLARE_DYNAMIC(CMainFrame)
// Overrides
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	virtual BOOL LoadFrame(UINT nIDResource, DWORD dwDefaultStyle = WS_OVERLAPPEDWINDOW | FWS_ADDTOTITLE, CWnd* pParentWnd = NULL, CCreateContext* pContext = NULL);
// Implementation
public:
	virtual ~CMainFrame();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	CStatusBar  m_wndStatusBar;
	CToolBar    m_wndToolBar;
	CChildView* chWnd;
	CString curFile;
public:
	CMathSheet* m_sh;
	CGrapher baseGraph;
protected:
	CToolDialog* tools;
// Generated message map functions
protected:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnFileClose();
	afx_msg void OnClose();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
public:
	afx_msg void OnSize(UINT nType, int cx, int cy);
public:
	void SetCoord(CString chX, CString chY);
public:
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
public:
	afx_msg void OnGraphic();
public:
	afx_msg void OnPaint();
	int GetScale();
	int GetNormScale();
	void OpenFile() throw (exception, invalid_argument);
	void SaveFile() throw (exception);
public:
	void ChildReDraw(void);
public:
	afx_msg void OnPreferences();
	afx_msg void OnToolsMath();
	afx_msg void OnDestroy();
	afx_msg void OnFileOpen();
	afx_msg void OnFileSave();
	afx_msg void OnToolsTrace();
};


