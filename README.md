UniUnitTest
=============

Unity で Unit Test を行うためのフレームワークです。


サンプルの実行
----------
1. UnityでUnityProjectを開きます
2. Sampleシーンを開きます
3. Playします
4. ログにテスト完了っぽいものがでたら終了です

* テストの内容はTest1.csの中に書いてあります。その中のHierarchyの構造を参考にしてください

使い方
----------
1. HierarchyにTestRunnerを配置します
2. HierarchyにTestSuiteを配置します
3. ２で配置したTestSuite以下にTestCaseを継承したクラス（例えばTest1.cs等）を配置します
4. TestCase継承クラスにアトリビュートにTestを指定してテスト関数を書きます
5. TestRunnerのプロパティRunSuiteに２で配置したTestSuiteをドラッグアンドドロップで設定します
6. Playします。
7. ログにテスト完了っぽいログがでたら終了です

補足
----------
* テスト関数にコルーチンを使うことができます
* テスト関数を呼ぶ前に同じクラスに[Setup]が指定されているメソッドがあれば呼びます（コルーチン可能です）
* テスト関数を呼んだ後に同じクラスに[TearDown]が指定されているメソッドがあれば呼びます（コルーチン可能です）
* コマンドラインから呼ぶことも可能です（TestEditorを参照してください）
* 検証は、Unity 4.3.0f4, mac/win で行いました

License
----------
Copyright © 2013 GREE Inc. Licensed under the MIT License.

Author
----------
Norifumi Okumura

