#define _CRT_SECURE_NO_WARNINGS

#include <cstdio>
//#include <set>
#include <string>
#include <map>

using namespace std;

map<string, int> list;

void wordUpdate(char** s) {
	//printf("%s ", s);
	string buf = (char*)s;
	string res;
	for (char i : buf) {
		if ((i >= 'a' && i <= 'z') || (i >= 'A' && i <= 'Z'))
		{
			if (i >= 'A' && i <= 'Z') res.push_back((char)((int)i+32));
			else res.push_back(i);
		}
	}
	if (list.find(res) != list.end()) list[res]++;
	else list[res] = 1;
	//printf("%s ", );
}

int main() {
	
	freopen("C:/Users/Mixael/Desktop/medium1.txt", "r", stdin);
	freopen("C:/Users/Mixael/Desktop/medium2.txt", "w", stdout);
	char* s[20];
	while (scanf("%s", &s) != EOF) {
		wordUpdate(s);
	}
	for (auto i : list) {
		//printf("%s %d\n", i.first.c_str(), i.second);
		printf("%s\n", i.first.c_str());
	}
}