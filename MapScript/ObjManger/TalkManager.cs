using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;// 사전이라는뜻
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(10, new string[] { "↑우리집 →깊은숲  " });
        talkData.Add(20, new string[] { "낙사<주의>", "이곳은 가지 않는게 좋겠어" });
        talkData.Add(30, new string[] { "지금은 앉고싶지않다." });

        talkData.Add(100, new string[] { "피아노는 좀 더 배워야겠어" });
        talkData.Add(200, new string[] { "그만자야겠다", "어서 집밖을 나가보자" });
        talkData.Add(500, new string[] { "이봐 너 지금 숲에 들어올려고 ?:0", "그럼 지금 날좀 도와주겠니?:1", "아주~간단한일이야:0","자 이거 받아 이건 수면 지팡이야:1",
        "지금 우리 숲은 불면증에 저주에 걸렸어:0","그 저주를 풀려면 이 수면마법을 쓸수있는 지팡이로 저주를 건 왕을 재워야해:1","하지만 이 지팡이는 저주에 걸리지 않은 사람만이 쓸수 있어:0",
        "걱정마 내가 옆에서 숲속 주민들을 잠재우는 방법을 알려줄게:1 ","자 어서 따라와:0"});

        talkData.Add(600, new string[] { "이봐 여기서 뭐하는거야 당장 꺼지지 못해?:0 ", "아니 왕자님이 왜 거기계십니까:1 ", "우리 왕자님을 데리고 어딜 다니는것이냐:0", "나 숲속의 경비대장으로 써 너에게 무력을 행사하겠다:1 " });


        talkData.Add(700, new string[] { "여기 부터는 못지나간다!:0 ", "인간주제에 여기까지 온건 칭찬해주지:1", "하지만 내가 있는이상 더는 못지나간다:0", "덤벼라 이몸은 이 숲속왕국에 기사단장이다:1" });

        talkData.Add(800, new string[] { "내가 여기까지 어떻게 왔는데 고작 인간 한명이 내 계획을 망치다니:0 ", "이 왕국은 자지 않고 계속 일해야만 게으른 놈들조차 자고싶으면 내말을 들을텐데:1", "다 네놈이 자초한 일이다 목숨으로 값아라:0" });

        portraitData.Add(500 + 0, portraitArr[0]);
        portraitData.Add(500 + 1, portraitArr[1]);

        portraitData.Add(600 + 0, portraitArr[0]);
        portraitData.Add(600 + 1, portraitArr[1]);

        portraitData.Add(700 + 0, portraitArr[0]);
        portraitData.Add(700 + 1, portraitArr[1]);

        portraitData.Add(800 + 0, portraitArr[0]);
        portraitData.Add(800 + 1, portraitArr[1]);
    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkIndex];
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {

        return portraitData[id + portraitIndex];
    }
}
