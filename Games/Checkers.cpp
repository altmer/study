#include "StdAfx.h"
#include "Checkers.h"

// some boolean function
bool cmp (Checkers::Piece*const& T1,Checkers::Piece*const& T2)
{
	if (T1->getBestMove()==T2->getBestMove()){
		if (T1->king!=T2->king){
			if (T1->getBestMove()<=Checkers::NOT_BEATEN)
				return T1->king;
			else{
				return !T1->king;
			}
		}
		if (T1->type==0){
			return T1->x>T2->x;
		}
		else{
			return T1->x<T2->x;
		}
	}
	return T1->getBestMove()<T2->getBestMove();
}

// Checkers implementation 

Checkers::Checkers(bool pvp)
{
	AI=0;
	if(!pvp){
		AI = new Player();
	}
}

Checkers::~Checkers(void)
{
	if (AI)
		delete AI;
}

void Checkers::run()
{
	system("cls");
	cout << " *** Welcome to Checkers ! ***" << endl;
	cout << endl;
	bool sel=false;
	while(!sel){
		int tmp;
		cout << "Please select your player: "<< endl;
		cout << "1. First player (Black). "<< endl;
		cout << "2. Second player (White). "<< endl;
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
					sel=true;
					break;
				case 2: isPlayer=false;
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
			if(!player_turn())
				return;
			isPlayer=false;
		}
		else{
			if(AI){
				AI->make_turn(pos, cMove);
			}
			else{
				if(!player_turn())
					return;
			}
			isPlayer=true;
		}

		cMove=1-cMove;

		system("cls");
		draw();
		if (!isPlayer)
			system("pause");

		int r=pos.res_game();

		switch (r){
			case F_WIN: 
				cout << "First player win!\n" << endl;
				fin=true;
				break;
			case S_WIN:
				cout << "Second player win!\n" << endl;
				fin=true;
				break;
		}
	}
	system("pause");
}

bool Checkers::player_turn()
{
	vector <Piece*> tmp;
	tmp=pos.kings(cMove);
	if (!tmp.empty()){
		cout << "You have the following kings: ";
		for_each(tmp.begin(), tmp.end(), mem_fun(&Piece::printXY));
		cout << "\n";
	}
	tmp=pos.toBeat(cMove);
	if (!tmp.empty()){
		cout << "You must make a move with one of the following pieces: ";
		for_each(tmp.begin(), tmp.end(), mem_fun(&Piece::printXY));
		cout << "\n";
	}
	cout << "\n";
	bool sel=false;
	while(!sel){
		cout << "Please, enter your move (for example, e2-e4): \n";
		string inp;
		
		cin>>inp;
		if(inp=="exit")
			return false;

		bool one=false;

		if (inp.size()>=5 && inp[0]>='a' && inp[0]<='h' && inp[1]>='1' && inp[1]<='8' && inp[3]>='a' && inp[3]<='h'
			&& inp[4]>='1' && inp[4]<='8'){
				int frx=inp[0]-'a', fry=inp[1]-'1', tox=inp[3]-'a', toy=inp[4]-'1';
				if (one && (tmp[0]->getCoord().first!=frx || tmp[0]->getCoord().second!=fry)){
					cout << "You must make a move with one of the following pieces: ";
					for_each(tmp.begin(), tmp.end(), mem_fun(&Piece::printXY));
					cout << "\n";
					continue;
				}
				if(pos.move(frx, fry, tox, toy,!tmp.empty(), cMove)){
					if(tmp.empty())
						sel=true;
					else{
						pos.toMove=pos.getPtr(tox,toy);
						pos.toMove->can_move(pos);
						if (pos.toMove->getBestMove() != Checkers::BEAT){
							sel=true;
						}
						else{
							tmp.clear();
							tmp.push_back(pos.toMove);
							one=true;
							system("cls");
							draw();
							cout << "You must make a move with one of the following pieces: ";
							for_each(tmp.begin(), tmp.end(), mem_fun(&Piece::printXY));
							cout << "\n";
						}
					}
				}
		}
		else{
			cout << "Please, enter move in form e2-e4!\n";
		}
	}
	return true;
}

void Checkers::draw()
{
	pos.draw();
}
// Piece implementation

int Checkers::Piece::dx[4]={ 1, 1,-1,-1};
int Checkers::Piece::dy[4]={-1, 1, -1, 1};
Checkers::Piece::Piece(int _x, int _y, int _t) : x(_x), y(_y), type(_t), king(false) {}

Checkers::Piece::~Piece() {}

int Checkers::Piece::can_move(Position& pos)
{
	int res=Checkers::CANNOT;
	if(king){
		for (int i=0; i<4; ++i){
			for (int cx=x+dx[i], cy=y+dy[i]; Position::on_board(cx, cy); cx+=dx[i], cy+=dy[i]){
				if (pos.grid[cx][cy]){
					if (pos.grid[cx][cy]->type==type){
						break;
					}
					else{
						if (Position::on_board(cx+dx[i],cy+dy[i]) && pos.grid[cx+dx[i]][cy+dy[i]]==0){
							return bestMove=Checkers::BEAT;
						}
						else{
							break;
						}
					}
				}
				else{
					if (pos.can_beat(cx, cy, 1-type)){
						res=min(res,Checkers::BEATEN);
					}
					else{
						res=min(res,Checkers::NOT_BEATEN);
					}
				}
			}
		}
		return bestMove=res;
	}
	for (int i=0; i<2; ++i){
		int cx=x+dx[i+2*type];
		int cy=y+dy[i+2*type];

		if(Position::on_board(cx, cy) && pos.grid[cx][cy]==0){
			if (pos.can_beat(cx, cy, 1-type)){
				res=min(res,Checkers::BEATEN);
			}
			else{
				res=min(res,Checkers::NOT_BEATEN);
			}
		}
	}
	for (int i=0; i<4; ++i){
		int cx=x+dx[i];
		int cy=y+dy[i];
		int c2x=x+2*dx[i];
		int c2y=y+2*dy[i];
		if(Position::on_board(cx, cy) &&  pos.grid[cx][cy] && pos.grid[cx][cy]->type!=type && 
			Position::on_board(c2x, c2y) && pos.grid[c2x][c2y]==0){
			return bestMove=Checkers::BEAT;
		}

	}
	return bestMove=res;
}

bool Checkers::Piece::beats(int cx, int cy, Position& pos)
{
	// test it
	if(king){
		if (abs(cx-x)==abs(cy-y)){
			int ddx=(cx-x)/abs(cx-x);
			int ddy=(cy-y)/abs(cy-y);
			for (int k=x+ddx, l=y+ddy; k!=cx || l!=cy; k+=ddx, l+=ddy){
				if (pos.grid[k][l]){
					if (pos.grid[k][l]->type==type)
						return false;
					else{
						if(k+ddx!=cx && k+ddy!=cy && pos.grid[k+ddx][k+ddy]){
							return false;
						}
					}
				}
			}
			int k=cx+ddx, l=cy+ddy;
			if (Position::on_board(k,l)){
				if(!pos.grid[k][l])
					return true;
				else
					return false;
			}
			else
				return false;
		}
	}
	else{
		if(abs(cx-x)==1 && abs(cy-y)==1){
			int ddx=(cx-x);
			int ddy=(cy-y);
			if (Position::on_board(x+2*ddx,y+2*ddy) && (!pos.grid[x+2*ddx][y+2*ddy] || pos.grid[x+2*ddx][y+2*ddy]==pos.toMove)){
				return true;
			}
			else{
				return false;
			}
		}
		else{
			return false;
		}
	}
}

void Checkers::Piece::printXY()
{
	cout << char ('a'+x) << y+1 <<  " ";
}
bool Checkers::Piece::operator== (const Piece& T) const
{
	return x==T.x && y==T.y;
}
void Checkers::Piece::move( int tox, int toy)
{
	x=tox;
	y=toy;
}
int Checkers::Piece::getBestMove()
{
	return bestMove;
}
pair <pair <int, int>, pair <int, int> > Checkers::Piece::getMove(Checkers::Position& pos)
{
	pos.toMove=this;
	int tox, toy;
	if (king){
		// add here evaluation of position!!!!!!!!!!!!!!!!!!!
		bool capt=(bestMove==Checkers::BEAT);
		bool beaten=true;
		int ccap=0;
		for (int i=0; i<4; ++i){
			bool captured=false;
			for (int cx=x+dx[i], cy=y+dy[i]; Position::on_board(cx, cy); cx+=dx[i], cy+=dy[i]){
				int cnt=0;
				if (pos.grid[cx][cy]){
					if (pos.grid[cx][cy]->type==type){
						break;
					}
					else{
						if (Position::on_board(cx+dx[i],cy+dy[i]) && pos.grid[cx+dx[i]][cy+dy[i]]==0){
							++cnt;
							if (beaten){
								ccap=cnt;
								tox=cx+dx[i];
								toy=cy+dy[i];
								captured=true;
								beaten=pos.can_beat(tox, toy, 1-type);
							}
							else if (cnt>ccap && !pos.can_beat(cx+dx[i],cy+dy[i], 1-type)){
								ccap=cnt;
								tox=cx+dx[i];
								toy=cy+dy[i];
								captured=true;
								beaten=pos.can_beat(tox, toy, 1-type);
							}
						}
						else{
							break;
						}
					}
				}
				else if (!capt || captured){
					if (pos.can_beat(cx, cy, 1-type)){
						if (beaten){
							tox=cx;
							toy=cy;
						}
					}
					else{
						if (beaten){
							tox=cx;
							toy=cy;
							beaten=false;
						}
					}
				}
			}
		}
	
	}
	else{
		bool beaten;
		if (bestMove==Checkers::BEAT){
			beaten=true;
			for (int i=0; i<4; ++i){
				int cx=x+dx[i];
				int cy=y+dy[i];
				int c2x=x+2*dx[i];
				int c2y=y+2*dy[i];
				if(Position::on_board(cx, cy) &&  pos.grid[cx][cy] && pos.grid[cx][cy]->type!=type && 
					Position::on_board(c2x, c2y) && pos.grid[c2x][c2y]==0){
						if (beaten){
							tox=c2x;
							toy=c2y;
							beaten=pos.can_beat(c2x,c2y,1-type);
						}
				}
			}
		}
		else{
			beaten=true;
			for (int i=0; i<2; ++i){
				int cx=x+dx[i+2*type];
				int cy=y+dy[i+2*type];
				if(Position::on_board(cx, cy) && pos.grid[cx][cy]==0){
					if (beaten){
						tox=cx;
						toy=cy;
						beaten=pos.can_beat(cx, cy, 1-type);
					}
				}
			}
		}
	}
	pos.toMove=0;
	return make_pair (make_pair(x,y), make_pair(tox, toy));
}
pair <int, int> Checkers::Piece::getCoord ()
{
	return make_pair(x,y);
}
//Position implementation

Checkers::Position::Position()
{
	grid.assign(8, vector <Piece*> (8, 0));
	// creating black piecess
	for (int x=0; x<3; ++x){
		for (int y=1-(x%2); y<8; y+=2){
			pPieces[Checkers::BLACK].push_back(Piece(x, y, Checkers::BLACK));
			grid[x][y]=&pPieces[Checkers::BLACK].back();
		}
	}
	// white pieces
	for (int x=7; x>=5; --x){
		for (int y=1-(x%2); y<8; y+=2){
			pPieces[Checkers::WHITE].push_back(Piece(x, y, Checkers::WHITE));
			grid[x][y]=&pPieces[Checkers::WHITE].back();
		}
	}
	toMove=0;
}

Checkers::Position::~Position(){}
bool Checkers::Position::on_board(int cx, int cy){
	return cx>=0 && cx<8 && cy>=0 && cy<8;
}


void Checkers::Position::draw()
{
	char ch=' ';
	printf (" ");
	for (int i=0; i<grid.size()*5+1; ++i){
		if(i%5==0){
			printf ("|");
		}
		else if(i%5==3){
			printf ("%d", i/5+1);
		}
		else{
			printf (" ");
		}
	}
	printf ("\n");
	
	printf ("-");
	for (int i=0; i<grid.size()*5+1; ++i){
		if(i%5==0)
			printf("+");
		else
			printf("-");
	}
	printf ("\n");
	for (int i=0; i<grid.size(); ++i){
		// first string
		printf (" ");
		for (int j=0; j<grid.size()*5+1; ++j){

			if((j/5-(1-(i%2)))%2==0){
				ch=' ';
			}
			else{
				ch='.';
			}

			if(j%5==0)
				printf("|");
			else{
				Piece* ptr=grid[i][j/5];
				if(ptr==0){
					printf ("%c", ch);
				}
				else if (ptr->type==Checkers::WHITE){
					if(j%5==1 || j%5==4){
						printf ("%c", ch);
					}
					else{
						printf ("-");
					}
				}
				else{
					if(j%5==1){
						printf("\\");
					}
					else if (j%5==4){
						printf ("/");
					}
					else{
						if(ptr->king)
							printf("-");
						else
							printf ("%c", ch);
					}
				}
			}
		}
		printf("\n");
		// second string
		printf ("%c", 'a'+i);
		for (int j=0; j<grid.size()*5+1; ++j){

			if((j/5-(1-(i%2)))%2==0){
				ch=' ';
			}
			else{
				ch='.';
			}

			if(j%5==0)
				printf("|");
			else{
				Piece* ptr=grid[i][j/5];
				if(ptr==0){
					printf ("%c", ch);
				}
				else if (ptr->type==Checkers::WHITE){
					if(j%5==1 || j%5==4){
						printf ("|");
					}
					else{
						if(ptr->king)
							printf ("-");
						else
							printf ("%c", ch);
					}
				}
				else{
					if(j%5==1 || j%5==4){
						printf ("%c", ch);
					}
					else if(j%5==2){
						printf("\\");
					}
					else if (j%5==3){
						printf ("/");
					}
				}
			}
		}
		printf("\n");
		// third string
		printf (" ");
		for (int j=0; j<grid.size()*5+1; ++j){

			if((j/5-(1-(i%2)))%2==0){
				ch=' ';
			}
			else{
				ch='.';
			}

			if(j%5==0)
				printf("|");
			else{
				Piece* ptr=grid[i][j/5];
				if(ptr==0){
					printf ("%c", ch);
				}
				else if (ptr->type==Checkers::WHITE){
					if(j%5==1 || j%5==4){
						printf ("|");
					}
					else{
						printf("%c", ch);
					}
				}
				else{
					if(j%5==1 || j%5==4){
						printf("%c", ch);
					}
					else if(j%5==2){
						printf("/");
					}
					else if (j%5==3){
						printf ("\\");
					}
				}
			}
		}
		printf("\n");
		// fourth string
		printf (" ");
		for (int j=0; j<grid.size()*5+1; ++j){

			if((j/5-(1-(i%2)))%2==0){
				ch=' ';
			}
			else{
				ch='.';
			}

			if(j%5==0)
				printf("|");
			else{
				Piece* ptr=grid[i][j/5];
				if(ptr==0){
					printf ("%c", ch);
				}
				else if (ptr->type==Checkers::WHITE){
					if(j%5==2 || j%5==3){
						printf ("-");
					}
					else{
						printf("%c", ch);
					}
				}
				else{
					if(j%5==2 || j%5==3){
						printf("%c", ch);
					}
					else if(j%5==1){
						printf("/");
					}
					else if (j%5==4){
						printf ("\\");
					}
				}
			}
		}
		printf("\n");
		// border
		printf ("-");
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

int Checkers::Position::res_game()
{
	for (int pl=Checkers::BLACK; pl<=Checkers::WHITE; ++pl){
		if(pPieces[pl].empty()){
			return 1-pl;
		}
		bool zap=true;
		for (list<Piece>::iterator it=pPieces[pl].begin(); it!=pPieces[pl].end(); ++it){
			toMove=&(*it);
			if (it->can_move(*this)!=Checkers::CANNOT){
				zap=false;
				break;
			}
			toMove=0;
		}
		if(zap)
			return 1-pl;
	}
	return Checkers::UNDETER;
}

bool Checkers::Position::can_beat (int x, int y, int pl)
{
	for (list<Piece>::iterator it=pPieces[pl].begin(); it!=pPieces[pl].end(); ++it){
		if (it->beats(x,y,*this))
			return true;
	}
	return false;
}


vector <Checkers::Piece*> Checkers::Position::kings(int pl)
{
	vector <Checkers::Piece*> ans;
	for (list<Piece>::iterator it=pPieces[pl].begin(); it!=pPieces[pl].end(); ++it){
		if (it->king){
			ans.push_back(&(*it));
		}
	}
	return ans;
}

vector <Checkers::Piece*> Checkers::Position::toBeat(int pl)
{
	vector <Piece*> ans;
	for (list<Piece>::iterator it=pPieces[pl].begin(); it!=pPieces[pl].end(); ++it){
		if (it->can_move(*this) == Checkers::BEAT){
			ans.push_back(&(*it));
		}
	}
	return ans;
}

bool Checkers::Position::move(int frx, int fry, int tox, int toy, bool capt, int pl)
{
	if (!grid[frx][fry]){
		cout << "There is no piece!\n";
		return false;
	}
	if (grid[frx][fry]->type!=pl){
		cout << "It's not your piece!\n";
		return false;
	}
	if (abs(tox-frx)!=abs(toy-fry)){
		cout << "You can move only by diagonals!\n";
		return false;
	}
	if (capt && abs(tox-frx)<2){
		cout << "You must beat smth in your turn!\n";
		return false;
	}
	if (grid[tox][toy]){
		cout << "You can't move there!\n";
		return false;
	}
	if (abs(tox-frx)>1){
		if(grid[frx][fry]->king){
			int ddx=(tox-frx)/abs(tox-frx);
			int ddy=(toy-fry)/abs(toy-fry);
			bool cap=false;
			// check for correctness
			for (int cx=frx+ddx, cy = fry+ddy; cx!=tox || cy!=toy; cx+=ddx,cy+=ddy){
				if (grid[cx][cy]){
					if (grid[cx][cy]->type==grid[frx][fry]->type){
						cout << "You can't overjump your pieces!\n";
						return false;
					}
					else{
						if(grid[cx+ddx][cy+ddy]){
							cout << "You can't overjump 2 pieces!\n";
							return false;
						}
						else{
							cap=true;
						}
					}
				}
			}
			if (!cap && cap!=capt){
				cout << "You must beat smth in your turn!\n";
				return false;
			}

			for (int cx=frx+ddx, cy = fry+ddy; cx!=tox || cy!=toy; cx+=ddx,cy+=ddy){
				if (grid[cx][cy]){
					del(grid[cx][cy]);
					grid[cx][cy]=0;
				}
			}
			grid[tox][toy]=grid[frx][fry];
			grid[tox][toy]->move(tox,toy);
			if (pl==0 && tox==7){
				grid[tox][toy]->king=true;
			}
			if (pl==1 && tox==0){
				grid[tox][toy]->king=true;
			}
			grid[frx][fry]=0;
			return true;
		}
		else{
			if(abs(tox-frx)!=2){
				cout << "It's only ordinary man, it's not a king!\n";
				return false;
			}
			int ddx=(tox-frx)/abs(tox-frx);
			int ddy=(toy-fry)/abs(toy-fry);
			if (!grid[frx+ddx][fry+ddy]){
				cout << "You can't move so far, it's not a king!\n";
				return false;
			}
			if (grid[frx+ddx][fry+ddy]->type==pl){
				cout << "You can't overjump your pieces!\n";
				return false;
			}
			del(grid[frx+ddx][fry+ddy]);
			grid[frx+ddx][fry+ddy]=0;
			grid[tox][toy]=grid[frx][fry];
			grid[tox][toy]->move(tox,toy);
			if (pl==0 && tox==7){
				grid[tox][toy]->king=true;
			}
			if (pl==1 && tox==0){
				grid[tox][toy]->king=true;
			}
			grid[frx][fry]=0;
			return true;
		}		
	}
	else{
		if (!grid[frx][fry]->king && tox-frx!=Piece::dx[2*pl]){
			cout << "You can't move in this direction, it's not a king!\n";
			return false;
		}
		grid[tox][toy]=grid[frx][fry];
		grid[tox][toy]->move(tox,toy);
		if (pl==0 && tox==7){
			grid[tox][toy]->king=true;
		}
		if (pl==1 && tox==0){
			grid[tox][toy]->king=true;
		}
		grid[frx][fry]=0;
		return true;
	}
}

void Checkers::Position::del( Checkers::Piece *ptr)
{
	pPieces[ptr->type].remove(*ptr);
}

vector <Checkers::Piece*> Checkers::Position::getMyPieces(int pl)
{
	vector <Checkers::Piece*> ret;
	for (list<Checkers::Piece>::iterator it=pPieces[pl].begin(); it!=pPieces[pl].end(); ++it){
		toMove=&(*it);
		it->can_move(*this);
		toMove=0;
		ret.push_back(&(*it));
	}
	return ret;
}
Checkers::Piece* Checkers::Position::getPtr(int x, int y)
{
	if (on_board(x,y))
		return grid[x][y];
	else
		return 0;
}
// Player implementation;

Checkers::Player::Player(){}

Checkers::Player::~Player() {}
void Checkers::Player::make_turn(Checkers::Position &pos, int pl)
{
	vector <Checkers::Piece*> v = pos.getMyPieces(pl);
	sort(v.begin(), v.end(), cmp);
	if ((*v.begin())->getBestMove()==Checkers::CANNOT)
		return;
	int tmp=v.front()->getBestMove();
	pair < pair <int, int>, pair <int, int> > ans= (v.front())->getMove(pos);
	pos.move(ans.first.first,ans.first.second,ans.second.first,ans.second.second,false, pl);
	system("cls");
	pos.draw();
	system("pause");
	pos.toMove=v.front();
	v.front()->can_move(pos);
	while (tmp==Checkers::BEAT && v.front()->getBestMove()==Checkers::BEAT){
		ans= (v.front())->getMove(pos);
		pos.move(ans.first.first,ans.first.second,ans.second.first,ans.second.second,false, pl);
		system("cls");
		pos.draw();
		system("pause");
		pos.toMove=v.front();
		v.front()->can_move(pos);
	}
	pos.toMove=0;
}