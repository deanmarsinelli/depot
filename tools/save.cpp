/*
	Save binary game data
*/

#include <cstdio>

struct character
{
	int position;
	int name;
	int lives;
} player1;


#define NCPUPLAYERS 4
character cpuPlayer[NCPUPLAYERS];

int saveVar1;
char saveVar2;
int saveVar3;
long long saveVar4;

void SaveGame()
{
	FILE* pFile = fopen("save.dat", "wb");
	if (!pFile)
	{
		return;
	}

	// save individual variables
	fwrite(&saveVar1, sizeof(saveVar1), 1, pFile);
	fwrite(&saveVar2, sizeof(saveVar2), 1, pFile);
	fwrite(&saveVar3, sizeof(saveVar3), 1, pFile);
	fwrite(&saveVar4, sizeof(saveVar4), 1, pFile);

	// save an object
	fwrite(&player1, sizeof(character), 1, pFile);
	
	// save an array of objects
	fwrite(cpuPlayer, sizeof(character), NCPUPLAYERS, pFile);

	fclose(pFile);
}

void LoadGame()
{
	FILE* pFile = fopen("save.dat", "rb");
	if (!pFile)
	{
		return;
	}

	// load individual variables
	fread(&saveVar1, sizeof(saveVar1), 1, pFile);
	fread(&saveVar2, sizeof(saveVar2), 1, pFile);
	fread(&saveVar3, sizeof(saveVar3), 1, pFile);
	fread(&saveVar4, sizeof(saveVar4), 1, pFile);

	// load an object
	fread(&player1, sizeof(character), 1, pFile);

	// load an array of objects
	fread(cpuPlayer, sizeof(character), NCPUPLAYERS, pFile);

	fclose(pFile);
}