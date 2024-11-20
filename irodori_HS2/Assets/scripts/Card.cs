using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
    public Sprite cardBack; // カードの裏面
    public Sprite cardFace; // カードの表面
    private bool isFaceUp = false; // カードが表向きかどうか
    private Image image; // Imageコンポーネント

    public static Card firstCard = null; // 最初にクリックされたカード
    public static Card secondCard = null; // 次にクリックされたカード
    private static bool canClick = true; // クリック可能かどうかのフラグ

    public delegate void CardMatchedHandler(Card card);
    public event CardMatchedHandler OnCardMatched;

    public delegate void CardFlippedHandler();
    public event CardFlippedHandler OnCardFlipped;

    public void Start()
    {
        image = GetComponent<Image>(); // Imageコンポーネントを取得
        ShowBack(); // 初期状態では裏面を表示
    }

    public void OnCardClicked()
    {
        if (!canClick || isFaceUp) return; // クリック不可またはすでに表向きの場合は処理を中断

        ShowFace(); // カードを表向きにする

        if (OnCardFlipped != null)
        {
            OnCardFlipped(); // カードをめくったイベントを呼び出す
        }

        if (firstCard == null)
        {
            firstCard = this; // 最初のカードを設定
        }
        else if (secondCard == null)
        {
            secondCard = this; // 次のカードを設定
            StartCoroutine(CheckMatch()); // 一致を確認するコルーチンを開始
        }
    }

    public void ShowFace()
    {
        image.sprite = cardFace; // 表面を表示
        isFaceUp = true; // 表向きフラグを設定
    }

    public void ShowBack()
    {
        image.sprite = cardBack; // 裏面を表示
        isFaceUp = false; // 表向きフラグをリセット
    }

    private IEnumerator CheckMatch()
    {
        canClick = false; // クリック不可に設定

        yield return new WaitForSeconds(1f); // 1秒待機

        if (firstCard.cardFace == secondCard.cardFace)
        {
            if (OnCardMatched != null)
            {
                OnCardMatched(firstCard); // 最初のカード一致イベントを呼び出す
                OnCardMatched(secondCard); // 次のカード一致イベントを呼び出す
            }
        }
        else
        {
            firstCard.ShowBack(); // 最初のカードを裏向きに戻す
            secondCard.ShowBack(); // 次のカードを裏向きに戻す
        }

        firstCard = null; // 最初のカードをリセット
        secondCard = null; // 次のカードをリセット
        canClick = true; // クリック可能に設定
    }
}
