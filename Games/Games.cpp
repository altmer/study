#include "stdafx.h"
#include "CxZ.h"
#include "Checkers.h"
#include "game.h"
using namespace std;

Game* G;

bool typesel()
{
	int tmp;
	while(true){
		system("cls");
		cout << "Please, select type of your game: \n";
		cout << "1. Player vs Computer. \n";
		cout << "2. Player vs Player.\n";
		cin >> tmp;
		if(cin.fail()){
			cin.clear();
			string badtoken;
			cin >> badtoken;
		}
		else{
			if(tmp>=1 && tmp<=2){
				return bool(tmp-1);
			}
		}
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	bool fin=false;
	while(!fin){
		system("cls");
		cout << "+------------------------+\n";
		cout << "|  Ikari's  Games  v1.0  |\n";
		cout << "+------------------------+\n";
		cout << "\n";
		cout << "Main menu: \n";
		cout << "1. Checkers.\n";
		cout << "2. Tic-Tac-Toe Classic.\n";
		cout << "3. Tic-Tac-Toe Extended.\n";
		cout << "4. Exit.\n";
		int tmp, n;
		bool sel;
		cin >> tmp;
		if(cin.fail()){
			cin.clear();
			string badtoken;
			cin >> badtoken;
		}
		else{
			switch(tmp){
				case 1:
					G = new Checkers(typesel());
					G->run();
					delete G;
					break;
				case 2: 
					G = new CxZ(3, true, typesel());
					G->run();
					delete G;
					break;
				case 3: 
					sel=false;
					while(!sel){
						system("cls");
						cout << "Please, enter the dimension of the grid (from 4 to 8): \n";
						cin >>n;
						if(cin.fail()){
							cin.clear();
							string badtoken;
							cin >> badtoken;
						}
						else{
							if(n>=4 && n<=8)
								sel=true;
						}
					}
					G = new CxZ(n, false, typesel());
					G->run();
					delete G;

					break;
				case 4:
					fin=true;
					break;
			}
		}
	}

	return 0;
}
