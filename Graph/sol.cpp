#include <iostream>
#include <sstream>
#include <cstdio>
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
	int StrToInt(const string& s);
	int WTF (string s);
	int WTO (char c);
	vector <int> split (string s);
public:
	CFunction();
	bool Construct(string s);
	double Calc (double x);
};

CFunction::CFunction(){}

int CFunction::StrToInt(const string& s)
{
	int pos=s.size()-1;
	int res=0;
	int mul=1;
	while (pos>=0){
		if (s[pos]=='-'){
			res=-res;
			break;
		}
		res+=(s[pos--]-'0')*mul;
		mul*=10;
	}
	return res;
}

int CFunction::WTF(string s)
{
	if (s=="cos"){
		return COS;
	}
	if (s=="sin"){
		return SIN;
	}
	if (s=="tan"){
		return TAN;
	}
	if (s=="ctg"){
		return CTG;
	}
	if (s=="exp"){
		return EXP;
	}
	return 0;
}
int CFunction::WTO(char c)
{
	if (c=='*')
		return MULT;
	if (c=='/')
		return DIV;
	if (c=='+')
		return PLUS;
	if (c=='-')
		return MINUS;
	return 0;
}
bool CFunction::Construct(string s)
{
	// delete whitespaces & uppercase letters
	for (int i=0; i<s.size(); ++i){
		if (isspace(s[i])){
			s.erase(i,1);
			--i;
		}
		else if (isalpha(s[i])){
			s[i]=tolower(s[i]);
		}
	}

	// check whether bracket expression is right
	int dif=0;
	for (int i=0; i<s.size(); ++i){
		if (s[i]=='('){
			++dif;
		}
		else if (s[i]==')'){
			--dif;
		}
	}

	// ERROR!
	if (dif){
		return false;
	}

	func=split(s);
	
	// ERROR!
	if (func.empty())
		return false;

	view=s;

	// All is OK!
	return true;
}

vector <int> CFunction::split(string s)
{
	vector <int> oper, tmp, binf;
	priority_queue <int> unf;
	int n_oper=0;
	int st=0, end=0;
	while (st<s.size()){
		//
		if (s[st]=='('){
			int num=0;
			for (end=st; end<s.size(); ++end){
				if (s[end]=='('){
					++num;
				}
				else if (s[end]==')'){
					--num;
				}
				if (!num)
					break;
			}
			tmp=split(s.substr(st+1, end-st-1));
			if (tmp.empty())
				return tmp;
			int t=oper.size();
			oper.resize(oper.size()+tmp.size());
			copy(tmp.begin(), tmp.end(), oper.begin()+t);
			n_oper++;

			// ERROR!
			if (n_oper>1 && binf.empty())
				return vector <int> ();
			
			if (!unf.empty()){
				oper.push_back(unf.front());
				unf.pop();
			}

			// ERROR!
			if (!unf.empty()){
				// ATTENTION
				if (unf.front()==UNMINUS){
					oper.push_back(unf.front());
					unf.pop();
				}
				else
					return vector <int>();
			}

			st=end+1;
		}
		//
		else if (isdigit(s[st])){
			for (end=st; end<s.size(); ++end){
				if (!isdigit(s[end]))
					break;
			}
			oper.push_back( StrToInt( s.substr(st, end-st) ) );
			n_oper++;

			// ERROR!
			if (n_oper>1 && binf.empty())
				return vector <int> ();

			st=end;
		}
		//
		else if (isalpha(s[st])){
			if (s[st]=='x'){
				end=st+1;

				oper.push_back(VAR);
				n_oper++;
				// ATTENTION
				if (!unf.empty() && unf.front()==UNMINUS){
					oper.push_back(unf.front());
					unf.pop();
				}

				// ERROR!
				if (n_oper>1 && binf.empty())
					return vector <int> ();

				if (end<s.size() && s[end]=='^'){
					st+=2;
					end=st;
					if (end<s.size() && isdigit(s[end])){
						for (; end<s.size(); ++end){
							if (!isdigit(s[end]))
								break;
						}
						oper.push_back(POW-StrToInt(s.substr(st, end-st) ) );
					}
					//ERROR!
					else{
						return vector<int>();
					}
				}
				st=end;
			}
			else{
				end=st+3;
				if (end<s.size()){
					int t=WTF(s.substr(st, end-st));
					//ERROR!
					if (!t)
						return vector <int>();

					unf.push( t );
				}
				//ERROR!
				else{
					return vector<int>();
				}
				st=end;
			}
		}
		//
		else if (s[st]=='*' || s[st]=='+'||s[st]=='-'||s[st]=='/'){
			if (binf.size()==2){
				if (n_oper==3){
					n_oper=2;
					oper.push_back(binf.back());
					binf.pop_back();					
				}
				// ERROR!
				else{
					return vector<int>();
				}
			}
			int op=WTO(s[st++]);

			// if there is no operands waiting
			if (!n_oper){
				if (op==MINUS){
					unf.push(UNMINUS);
				}
				// ERROR!
				else{
					return vector <int>();
				}
			}
			else if (n_oper==1){
				if (binf.empty())
					binf.push_back(op);
				// ERROR!
				else
					return vector<int>();
			}
			else if (n_oper==2){
				if (binf.size()==1){
					if ( (binf.back()==MINUS || binf.back()==PLUS) && (op==MULT || op==DIV) ){
						binf.push_back(op);
					}
					else{
						n_oper=1;
						oper.push_back(binf.back());
						binf.back()=op;
					}
				}
				// ERROR!
				else{
					return vector <int>();
				}
			}
			// ERROR!
			else
				return vector <int>();
		}
		//
		else{
			oper.clear();
			return oper;
		}
		// end of while
	}
	// end of method
	if (n_oper>1){
		if (n_oper==2){
			if (!binf.empty()){
				oper.push_back(binf.back());
			}
			else
				return vector<int>();
		}
		else if (n_oper==3){
			if (binf.size()==2){
				oper.push_back(binf.back());
				oper.push_back(*binf.begin());
			}
			else
				return vector <int>();
		}
		else
			return vector <int>();
	}
	if (!unf.empty())
		return vector <int>();
	return oper;
}

double CFunction::Calc(double x)
{
	vector <double> res;
	for (int i=0; i<func.size(); ++i){
		if (func[i]>=0){
			res.push_back(func[i]);
		}
		else if (func[i]>=MINUS){
			switch(func[i]){
				// ERRRO!
				if (res.size()<2)
					return 1e+9;
				case MINUS: res[res.size()-2]-=res.back();
					res.pop_back();
					break;
				case PLUS: res[res.size()-2]+=res.back();
					res.pop_back();
					break;
				case MULT: res[res.size()-2]*=res.back();
					res.pop_back();
					break;
				case DIV: res[res.size()-2]/=res.back();
					res.pop_back();
					break;
			}
		}
		else if (func[i]>=EXP){
			switch(func[i]){
				case COS: res.back()=cos(res.back());
					break;
				case TAN: 
					if (fabs(cos(res.back()))<1e-6)
						return 1e+9;
					else
						res.back()=sin(res.back())/cos(res.back());
					break;
				case CTG: 
					if (fabs(sin(res.back()))<1e-6)
						return 1e+9;
					else
						res.back()=cos(res.back())/sin(res.back());
					break;
				case SIN: res.back()=sin(res.back());
					break;
				case EXP: res.back()=exp(res.back());
					break;
				case UNMINUS: res.back()=-res.back();
					break;
			}
		}
		else if (func[i]==VAR){
			res.push_back(x);
		}
		else{
			res.back()=pow(res.back(), double(POW-func[i]));
		}
	}
	
	if (res.size()!=1)
		return 1e+9;

	return res.back();
}
CFunction solve;
string s;

int main (void)
{
	freopen ("input.txt", "r", stdin);
	freopen ("output.txt", "w", stdout);

	getline (cin,s);
	double t;
	if (solve.Construct(s))
		t = solve.Calc(2);
	return 0;
}