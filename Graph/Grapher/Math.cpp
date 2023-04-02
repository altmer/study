#include "StdAfx.h"
#include "Math.h"
#include "Function.h"
#include <vector>
#include <algorithm>
#include <cmath>
#include <exception>
using namespace std;

const double eps = 1e-9;
double n = 22861;

Math::Math(void)
{
}

Math::~Math(void)
{
}
 double Math::sol_half (double a, double b, CFunction& f)
{
	double x=(a+b)/2.0;
	if (f(a)*f(x)<0)
		b=x;
	else 
		a=x;
	if (fabs(a-b)<eps)
		return a;
	else
		return sol_half (a, b, f);
}

 vector <double> Math::findRoot(double a, double b, CFunction& f)throw (invalid_argument)
{
	if (a==b)
		throw invalid_argument("Interval must be not empty!");
	if (a>b)
		throw invalid_argument("Wrong interval!");
	vector <double> ret;
	double h=(b-a)/n;
	for (int i=0; i<=n; ++i)
	{
		double x=a+i*h;
		double x2=a+(i+1)*h;
		if (f(x)*f(x2)<0){
			ret.push_back(sol_half(x, x2, f));
			if (fabs(ret.back())<eps)
				ret.back()=fabs(ret.back());
		}
	}
	sort(ret.begin(), ret.end());
	return ret;
}

 double Math::integrate(double a, double b, CFunction &f)throw (invalid_argument)
{
	if (a==b)
		throw invalid_argument("Interval must be not empty!");
	if (a>b)
		throw invalid_argument("Wrong interval!");
	double h=(b-a)/n;
	double I=0;
	for (int i=0; i<n; ++i)
	{
		double x=a+i*h+h/2;
		I+=f(x);
	}
	return h*I;
}