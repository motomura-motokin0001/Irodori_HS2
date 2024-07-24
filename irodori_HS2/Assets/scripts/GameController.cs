using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab; // カードのプレハブ
    public List<Sprite> cardFaces; // カードの表面スプライトのリスト
    public Transform cardParent; // カードを配置する親オブジェクト
    public TextMeshProUGUI clearText; // Clearテキストの参照
    public TextMeshProUGUI countText; // カードをめくった回数のテキストの参照
    public int columns = 4; // カードの列数
    public float spacing = 10f; // カード間のスペース

    private List<GameObject> cards = new List<GameObject>(); // 生成されたカードのリスト
    private int flipCount = 0; // カードをめくった回数

    void Start()
    {
        SetupGame(); // ゲームのセットアップを開始
        clearText.enabled = false; // 初期状態ではClearテキストを非表示にする
        UpdateFlipCountText(); // カードをめくった回数のテキストを更新
    }

    void SetupGame()
    {
        List<Sprite> deck = new List<Sprite>();

        // デッキにカードのペアを追加
        for (int i = 0; i < 2; i++)
        {
            foreach (var face in cardFaces)
            {
                deck.Add(face);
            }
        }

        Shuffle(deck); // デッキをシャッフル

        // カードの生成と配置
        float cardWidth = cardPrefab.GetComponent<RectTransform>().rect.width;
        float cardHeight = cardPrefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < deck.Count; i++)
        {
            int row = i / columns;
            int column = i % columns;

            Vector3 position = new Vector3(
                column * (cardWidth + spacing),
                -row * (cardHeight + spacing),
                0f
            );

            GameObject card = Instantiate(cardPrefab, cardParent);
            card.GetComponent<RectTransform>().anchoredPosition = position;
            card.GetComponent<Card>().cardFace = deck[i];
            card.GetComponent<Card>().OnCardMatched += OnCardMatched; // カード一致イベントを購読
            card.GetComponent<Card>().OnCardFlipped += OnCardFlipped; // カードをめくったイベントを購読
            cards.Add(card);
        }
    }

    // フィッシャー・イェーツのシャッフルアルゴリズム
    void Shuffle(List<Sprite> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Sprite temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // カードが一致したときに呼ばれるメソッド
    void OnCardMatched(Card card)
    {
        cards.Remove(card.gameObject);
        Destroy(card.gameObject);

        // すべてのカードが削除された場合、clearテキストを表示する
        if (cards.Count == 0)
        {
            clearText.enabled = true;
        }
    }

    // カードをめくったときに呼ばれるメソッド
    void OnCardFlipped()
    {
        flipCount++;
        UpdateFlipCountText(); // カードをめくった回数のテキストを更新
    }

    // カードをめくった回数のテキストを更新するメソッド
    void UpdateFlipCountText()
    {
        countText.text = "Flips: " + flipCount;
    }
}
