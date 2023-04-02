#include "StdAfx.h"
#include "CxZ.h"


CxZ::Player::Player(){}
CxZ::Player::~Player(){}
// Constructors
int CxZ::tow=0;
CxZ::CxZ(int n, bool h, bool isPvP) : cPos(n)
{
	rasm=n;
	sym[0]='X';
	sym[1]='O';
	cMove=0;
	if(h)
		CxZ::tow=3;
	else
		CxZ::tow=4;
	if(!isPvP){
		if (h){
			AI = new CxZ::PlayerHARD();
			AI->init(n);
		}
		else{
			AI = new CxZ::PlayerEASY();
			AI->init(n);
		}
	}
	else
		AI=0;
}

CxZ::~CxZ(void)
{
	if(AI)
		delete AI;
}

// Position implementation

CxZ::Position::Position(int n)
{
	grid.assign(n, vector <char> (n, '.'));
}

CxZ::Position::~Position()
{
}

void CxZ::Position::set (char ch, int wh)
{
	grid[wh/(int)grid.size()][wh%(int)grid.size()]=ch;
}

void CxZ::Position::clear (int wh)
{
	grid[wh/(int)grid.size()][wh%(int)grid.size()]='.';
}

bool CxZ::Position::isFree (int wh)
{
	return grid[wh/(int)grid.size()][wh%(int)grid.size()]=='.';
}

int CxZ::Position::size()
{
	return grid.size();
}

int CxZ::Position::res_game ()
{
	int nsp=0;
	for (int i=0; i<grid.size(); ++i){
		for (int j=0; j<grid[i].size(); ++j){
			if (grid[i][j]=='.')
				++nsp;
		}
	}
	// horiz
	for (int i=0; i<grid.size(); ++i){
		char tmp=grid[i][0];
		int cnt=1;
		for (int j=1; j<grid[i].size(); ++j){
			if (grid[i][j]==grid[i][j-1]){
				cnt++;
			}
			else{
				tmp=grid[i][j];
				cnt=1;
			}
			if(cnt==CxZ::tow && tmp!='.'){
				if (tmp=='X')
					return CxZ::F_WIN;
				else
					return CxZ::S_WIN;
			}
		}
	}
	// vert
	for (int i=0; i<grid[0].size(); ++i){
		char tmp=grid[0][i];
		int cnt=1;
		for (int j=1; j<grid.size(); ++j){
			if (grid[j][i]==grid[j-1][i]){
				++cnt;
			}
			else{
				tmp=grid[j][i];
				cnt=1;
			}
			if (cnt==CxZ::tow && tmp!='.'){
				if (tmp=='X')
					return CxZ::F_WIN;
				else
					return CxZ::S_WIN;
			}
		}
	}
	// main diag
	for (int i=0; i<=grid.size()-CxZ::tow; ++i){
		for (int j=0; j<=grid.size()-CxZ::tow; ++j){
			char tmp=grid[i][j];
			int cnt=1;
			for (int k=1; i+k<grid.size() && j+k<grid.size(); ++k){
				if (grid[i+k][j+k]==grid[i+k-1][j+k-1]){
					++cnt;
				}
				else{
					tmp=grid[i+k][j+k];
					cnt=1;
				}
				if (cnt==CxZ::tow && tmp!='.'){
					if (tmp=='X')
						return CxZ::F_WIN;
					else
						return CxZ::S_WIN;
				}
			}
		}
	}
	for (int i=0; i<=grid.size()-CxZ::tow; ++i){
		for (int j=CxZ::tow-1; j<grid.size(); ++j){
			char tmp=grid[i][j];
			int cnt=1;
			for (int k=1; i+k<grid.size() && j-k>=0; ++k){
				if (grid[i+k][j-k]==grid[i+k-1][j-k+1]){
					++cnt;
				}
				else{
					tmp=grid[i+k][j-k];
					cnt=1;
				}
				if (cnt==CxZ::tow && tmp!='.'){
					if (tmp=='X')
						return CxZ::F_WIN;
					else
						return CxZ::S_WIN;
				}
			}
		}
	}
	if (nsp)
		return CxZ::UNDETER;
	else
		return CxZ::DRAW;
}

char CxZ::Position::get(int wh)
{
	return grid[wh/(int)grid.size()][wh%(int)grid.size()];
}

void CxZ::Position::unhash(long long h)
{
	for (int i=0; i<grid.size(); ++i){
		for (int j=0; j<grid[i].size(); ++j){
			switch(h%4){
				case 0: grid[i][j]='.';
					break;
				case 1: grid[i][j]='O';
					break;
				case 2: grid[i][j]='X';
			}
			h>>=2;
		}
	}
}

void CxZ::Position::draw()
{
	for (int i=0; i<grid.size()*5+1; ++i){
		if(i%5==0)
			printf("+");
		else
			printf("-");
	}
	printf ("\n");
	for (int i=0; i<grid.size(); ++i){
		// first string
		for (int j=0; j<grid.size()*5+1; ++j){
			if(j%5==0)
				printf("|");
			else{
				char ch=get(i*grid.size()+j/5);
				if(ch=='X'){
					if(j%5==1){
						printf("\\");
					}
					else if(j%5==2){
						printf("%2d", i*grid.size()+j/5+1);
					}
					else if (j%5==4){
						printf ("/");
					}
				}
				else if (ch=='O'){
					if(j%5==1 || j%5==4){
						printf (" ");
					}
					else{
						printf ("-");
					}
				}
				else{
					if(j%5==2)
						printf("%2d", i*grid.size()+j/5+1);
					else if (j%5!=3)
						printf(" ");
				}
			}
		}
		printf("\n");
		// second string
		for (int j=0; j<grid.size()*5+1; ++j){
			if(j%5==0)
				printf("|");
			else{
				char ch=get(i*grid.size()+j/5);
				if(ch=='X'){
					if(j%5==1 || j%5==4){
						printf(" ");
					}
					else if(j%5==2){
						printf("\\");
					}
					else if (j%5==3){
						printf ("/");
					}
				}
				else if (ch=='O'){
					if(j%5==1 || j%5==4){
						printf ("|");
					}
					else if (j%5==2){
						printf("%2d", i*grid.size()+j/5+1);
					}
				}
				else{
					printf(" ");
				}
			}
		}
		printf("\n");
		// third string
		for (int j=0; j<grid.size()*5+1; ++j){
			if(j%5==0)
				printf("|");
			else{
				char ch=get(i*grid.size()+j/5);
				if(ch=='X'){
					if(j%5==1 || j%5==4){
						printf(" ");
					}
					else if(j%5==2){
						printf("/");
					}
					else if (j%5==3){
						printf ("\\");
					}
				}
				else if (ch=='O'){
					if(j%5==1 || j%5==4){
						printf ("|");
					}
					else{
						printf(" ");
					}
				}
				else{
					printf(" ");
				}
			}
		}
		printf("\n");
		// fourth string
		for (int j=0; j<grid.size()*5+1; ++j){
			if(j%5==0)
				printf("|");
			else{
				char ch=get(i*grid.size()+j/5);
				if(ch=='X'){
					if(j%5==2 || j%5==3){
						printf(" ");
					}
					else if(j%5==1){
						printf("/");
					}
					else if (j%5==4){
						printf ("\\");
					}
				}
				else if (ch=='O'){
					if(j%5==2 || j%5==3){
						printf ("-");
					}
					else{
						printf(" ");
					}
				}
				else{
					printf(" ");
				}
			}
		}
		printf("\n");
		// border
		for (int j=0; j<grid.size()*5+1; ++j){
			if(j%5==0)
				printf("+");
			else
				printf("-");
		}
		printf("\n");
	}
	printf("\n");

}

long long CxZ::Position::hash()
{
	long long bas=1;
	long long ret=0;
	for (int i=0; i<grid.size(); ++i){
		for (int j=0; j<grid[i].size(); ++j){
			switch(grid[i][j]){
				case 'O': ret+=bas;
					break;
				case 'X': ret+=bas<<1;
					break;
			}
			bas<<=2;
		}
	}
	return ret;
}

// PlayerHARD implementation

CxZ::PlayerHARD::PlayerHARD() 
{
}

void CxZ::PlayerHARD::init(int n)
{
	int cnt=0;
	Position cur(n);

	int tmp=cur.hash();
	pos_num[tmp]=cnt++;
	tree.push_back(vector <int> ());
	codes.push_back(tmp);
	win.push_back(cur.res_game());
	turn.push_back(0);

	queue<Position> q;
	q.push(cur);

	// building game tree
	while (!q.empty()){
		cur=q.front();
		q.pop();

		if (cur.res_game()!=CxZ::UNDETER)
			continue;

		int fr=pos_num[cur.hash()];
		for (int i=0; i<n*n; ++i){
			if (cur.isFree(i)){
				if(turn[fr]==0){
					cur.set('X', i);
					tmp=cur.hash();

					if(pos_num.count(tmp)==0){
						pos_num[tmp]=cnt++;
						tree.push_back(vector <int> ());
						codes.push_back(tmp);
						win.push_back(cur.res_game());
						turn.push_back(1-turn[fr]);
						q.push(cur);
					}

					tree[fr].push_back(pos_num[tmp]);
					cur.clear(i);
				}
				else{

					cur.set('O', i);
					tmp=cur.hash();

					if(pos_num.count(tmp)==0){
						pos_num[tmp]=cnt++;
						tree.push_back(vector <int> ());
						turn.push_back(1-turn[fr]);
						codes.push_back(tmp);
						win.push_back(cur.res_game());
						q.push(cur);
					}
					tree[fr].push_back(pos_num[tmp]);
					cur.clear(i);
				}
			}
		}
	}
}

int CxZ::PlayerHARD::dfs1 (int u){
	if (res[u]!=-1)
		return res[u];
	if (win[u]!=CxZ::UNDETER){
		return res[u]=win[u];
	}
	res[u]=CxZ::S_WIN;
	for (int i=0; i<tree[u].size(); ++i){
		int tm=dfs2(tree[u][i]);
		if (res[u]==CxZ::S_WIN && tm==CxZ::DRAW){
			res[u]=CxZ::DRAW;			
		}
		if (tm==CxZ::F_WIN){
			return res[u]=CxZ::F_WIN;
		}
	}
	return res[u];
}

int CxZ::PlayerHARD::dfs2 (int u){
	if (res[u]!=-1)
		return res[u];
	if (win[u]!=CxZ::UNDETER){
		return res[u]=win[u];
	}
	res[u]=CxZ::F_WIN;
	for (int i=0; i<tree[u].size(); ++i){
		int tm=dfs1(tree[u][i]);
		if (res[u]==CxZ::F_WIN && tm==CxZ::DRAW){
			res[u]=CxZ::DRAW;			
		}
		if (tm==CxZ::S_WIN){
			return res[u]=CxZ::S_WIN;
		}
	}
	return res[u];
}

void CxZ::PlayerHARD::make_turn(CxZ::Position &pos, int pl)
{
	res.assign(win.size(), -1);
	int cur = pos_num[pos.hash()];
	int bestnum=-1;
	int bestres=-1;
	
	for (int i=0; i<tree[cur].size(); ++i){
		int tm;
		if(turn[cur]==0){
			tm = dfs2(tree[cur][i]);
			if(bestres==-1 && tm==CxZ::S_WIN){
				bestnum=tree[cur][i];
			}
			if(bestres==-1 && tm==CxZ::DRAW){
				bestres=CxZ::DRAW;
				bestnum=tree[cur][i];
			}
			if (tm==CxZ::F_WIN){
				bestres=CxZ::F_WIN;
				bestnum=tree[cur][i];
				break;
			}
		}
		else{
			tm = dfs1(tree[cur][i]);
			if(bestres==-1 && tm==CxZ::F_WIN){
				bestnum=tree[cur][i];
			}
			if(bestres==-1 && tm==CxZ::DRAW){
				bestres=CxZ::DRAW;
				bestnum=tree[cur][i];
			}
			if (tm==CxZ::S_WIN){
				bestres=CxZ::S_WIN;
				bestnum=tree[cur][i];
				break;
			}
		}
	}

	pos.unhash(codes[bestnum]);
}

CxZ::PlayerHARD::~PlayerHARD() {}

// Crosses and Zeroes implementation

void CxZ::run()
{
	system("cls");
	cout << " *** Welcome to Tic-Tac-Toe ! ***" << endl;
	cout << endl;
	int pl;
	bool sel=false;
	while(!sel){
		int tmp;
		cout << "Please select your player: "<< endl;
		cout << "1. First player (X's). "<< endl;
		cout << "2. Second player (O's). "<< endl;
		cout << "3. Exit. "<< endl;
		cin >> tmp;
		if(cin.fail()){
			cin.clear();
			string badtoken;
			cin >> badtoken;
		}
		else{
			switch(tmp){
				case 1: isPlayer=true;
					pl=tmp;
					sel=true;
					break;
				case 2: isPlayer=false;
					pl=tmp;
					sel=true;
					break;
				case 3:
					return;
			}
		}
		if(!sel){
			system("cls");
			cout << "Wrong answer! Try again." << endl;
		}
	}

	cMove=0;
	bool fin=false;
	while(!fin){
		system("cls");
		draw();
		if(cMove==0)
			cout << "First palyer's turn\n";
		else
			cout << "Second player's turn.\n";
		if (isPlayer){
			player_turn();
			isPlayer=false;
		}
		else{
			if(AI)
				AI->make_turn(cPos, cMove);
			else
				player_turn();
			isPlayer=true;
		}
		cMove=1-cMove;
		system("cls");
		draw();
		system("pause");
		int r=cPos.res_game();
		switch (r){
			case F_WIN: 
					cout << "First player win!\n" << endl;
					fin=true;
					break;
			case S_WIN:
					cout << "Second player win!\n" << endl;
					fin=true;
				break;
			case DRAW:
				cout << "Draw!\n" << endl;
				fin=true;
				break;
		}
	}
	system("pause");
}

void CxZ::draw()
{
	cPos.draw();
}

void CxZ::player_turn()
{
	bool sel=false;
	while(!sel){
		int tmp;
		cout << "Please enter your move (a number of cell): "<< endl;
		cin >> tmp;
		if(cin.fail()){
			cin.clear();
			string badtoken;
			cin >> badtoken;
		}
		else{
			if (tmp>=1 && tmp<=rasm*rasm){
				if (cPos.isFree(tmp-1)){
					cPos.set(sym[cMove], tmp-1);
					sel=true;
				}
			}
		}
		if(!sel){
			system("cls");
			draw();
			cout << "Wrong move! Try again.\n" << endl;
		}
	}
}

// PlayerEASY implementation

CxZ::PlayerEASY::PlayerEASY()
{
}

CxZ::PlayerEASY::~PlayerEASY()
{
}
void CxZ::PlayerEASY::init(int n)
{
}

int CxZ::PlayerEASY::count( vector<char>const& bef,  vector<char>const& aft, char ch) const{
	int ret=0;

	// to win now!

	if(aft.size()>=2 && bef.size()>=1 && aft[0]==aft[1] && aft[0]==bef[0]){
		if (aft[0]==ch)
			ret+=1000000;
		else if (aft[0]!='.')
			ret+=100000;
	}
	if(aft.size()>=1 && bef.size()>=2 && bef[0]==bef[1] && aft[0]==bef[0]){
		if (bef[0]==ch)
			ret+=1000000;
		else if (aft[0]!='.')
			ret+=100000;
	}
	if( aft.size()==3 && aft[0]==aft[1] && aft[0]==aft[2]){
		if (aft[0]==ch)
			ret+=1000000;
		else if (aft[0]!='.')
			ret+=100000;
	}
	if( bef.size()==3 && bef[0]==bef[1] && bef[0]==bef[2]){
		if (bef[0]==ch)
			ret+=1000000;
		else if (bef[0]!='.')
			ret+=100000;
	}

	// to win soon!

	if (aft.size()>=2 && bef.size()>=2 && aft[0]==bef[0] && aft[1]=='.' && bef[1]=='.'){
		if(aft[0]==ch){
			ret+=1000;
		}
		else if (aft[0]!='.'){
			ret+=10000;
		}
	}
	if(aft.size()>=3 && bef.size()>=1 && aft[0]==aft[1] && aft[2]=='.' && bef[0]=='.'){
		if(aft[0]==ch)
			ret+=1000;
		else if (aft[0]!='.')
			ret+=10000;
	}
	if(bef.size()>=3 && aft.size()>=1 && bef[0]==bef[1] && bef[2]=='.' && aft[0]=='.'){
		if(bef[0]==ch)
			ret+=1000;
		else if (bef[0]!='.')
			ret+=10000;
	}

	if(!bef.empty()){
		int k=0;

		while(k<bef.size()){
			if (bef[k]==ch){
				ret+=10;
			}
			else if (bef[k]=='.'){
				ret+=5;
			}
			else{
				break;
			}
			++k;
		}
		k=0;
		while(k<bef.size() && bef[k]!=ch){
			if(bef[k]!='.')
				ret++;
			++k;
		}
	}

	if(!aft.empty()){
		int k=0;

		while(k<aft.size()){
			if (aft[k]==ch){
				ret+=10;
			}
			else if (aft[k]=='.'){
				ret+=5;
			}
			else{
				break;
			}
			++k;
		}
		k=0;
		while(k<aft.size() && aft[k]!=ch){
			if(aft[k]!='.')
				ret++;
			++k;
		}
	}
	return ret;
}
void CxZ::PlayerEASY::make_turn(CxZ::Position &pos, int pl)
{
	char ch;
	if(pl==0)
		ch='X';
	else
		ch='O';
	vector <vector <int> > eval(pos.size(), vector <int> (pos.size(), 0));

	for (int i=0; i<eval.size(); ++i){
		for (int j=0; j<eval[i].size(); ++j){
			vector <char> bef, aft;
			if(pos.isFree(i*eval.size()+j)){
				eval[i][j]=1;
				// horiz
				for (int k=1; k<=3 && j+k<eval.size(); ++k){
					aft.push_back(pos.get(i*eval.size()+j+k));
				}
				for (int k=1; k<=3 && j-k>=0; ++k){
					bef.push_back(pos.get(i*eval.size()+j-k));
				}
				eval[i][j]+=count(bef, aft, ch);
				// vert
				bef.clear();
				aft.clear();
				for (int k=1; k<=3 && i+k<eval.size(); ++k){
					aft.push_back(pos.get((i+k)*eval.size()+j));
				}
				for (int k=1; k<=3 && i-k>=0; ++k){
					bef.push_back(pos.get((i-k)*eval.size()+j));
				}
				eval[i][j]+=count(bef, aft, ch);
				//diag
				bef.clear();
				aft.clear();
				for (int k=1; k<=3 && i+k<eval.size() && j+k<eval.size(); ++k){
					aft.push_back(pos.get((i+k)*eval.size()+k+j));
				}
				for (int k=1; k<=3 && i-k>=0 && j-k>=0; ++k){
					bef.push_back(pos.get((i-k)*eval.size()+j-k));
				}
				eval[i][j]+=count(bef, aft, ch);
				bef.clear();
				aft.clear();
				for (int k=1; k<=3 && i+k<eval.size() && j-k>=0; ++k){
					aft.push_back(pos.get((i+k)*eval.size()+j-k));
				}
				for (int k=1; k<=3 && i-k>=0 && j+k<eval.size(); ++k){
					bef.push_back(pos.get((i-k)*eval.size()+j+k));
				}
				eval[i][j]+=count(bef, aft, ch);
			}
		}
	}
	vector <pair<int,int> > ans;
	int c_ans=-1;
	for (int i=0; i<eval.size(); ++i){
		for (int j=0; j<eval.size(); ++j){
			if(eval[i][j]>c_ans){
				ans.clear();
				ans.push_back(make_pair(i,j));
				c_ans=eval[i][j];
			}
			else if (eval[i][j]==c_ans){
				ans.push_back(make_pair(i,j));
			}
		}
	}
	int sel=rand()%ans.size();
	pos.set(ch, ans[sel].first*eval.size()+ans[sel].second);
}