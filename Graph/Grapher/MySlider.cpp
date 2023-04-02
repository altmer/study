// MySlider.cpp : implementation file
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "MySlider.h"
#include "Tracer.h"


// CMySlider

IMPLEMENT_DYNAMIC(CMySlider, CSliderCtrl)

CMySlider::CMySlider(CWnd* par )
{
	p=par;
}

CMySlider::~CMySlider()
{
}


BEGIN_MESSAGE_MAP(CMySlider, CSliderCtrl)
	ON_WM_MOUSEMOVE()
END_MESSAGE_MAP()



// CMySlider message handlers



void CMySlider::OnMouseMove(UINT nFlags, CPoint point)
{
	// TODO: Add your message handler code here and/or call default

	if (p!=NULL){
		CTracer* p2 = (CTracer*)p;
		p2->OnMouseMove(nFlags, point);
	}
	CSliderCtrl::OnMouseMove(nFlags, point);
}
