using System;

using UnityEngine;

using UniRx;

public class SubjectSample : MonoBehaviour
{
    private Subject<Unit> subject;
    //! 登録した関数を保存する
    private IDisposable disposable;
    // Use this for initialization
    void Start()
    {
        //! Subjectオブジェクトを作成
        subject = new Subject<Unit>();
        disposable = null;
    }

    /// <summary>
    /// ログを表示する関数を登録する
    /// </summary>
    public void SubscribeButton()
    {
        //! 1回しかSubscribeできないようにする
        if (disposable == null)
        {
            Debug.Log("Subscribe");

            //! Subscribeした内容をdisposableに保存
            disposable = subject.Subscribe(_ => Debug.Log("Hello World"));
        }
    }

    /// <summary>
    /// 登録した関数を除去する
    /// </summary>
    public void DisposeButton()
    {
        if (disposable != null)
        {
            Debug.Log("Dispose");

            //! これだとsubject本体をDisposeしてしまう
            //subject.Dispose();

            //! 正しくはこちら
            disposable.Dispose();

            //! 再度、Subscribeできるように
            disposable = null;
        }
    }

    /// <summary>
    /// 登録されている関数を発火する
    /// </summary>
    public void OnNextButton()
    {
        subject.OnNext(Unit.Default);
    }
}