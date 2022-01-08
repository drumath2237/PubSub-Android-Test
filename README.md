# PubSub-Android-Test

## About

Azure Web PubSub を Unity IL2CPP Android で使えるか試すだけ。
多分 Android じゃなくても Mono でも動く気がしている。

[Azure Web PubSub](https://azure.microsoft.com/ja-jp/services/web-pubsub/)はリアルタイム双方向通信を実現するための Pub/Sub ソリューションで、現在 Azure の β 機能として提供されています。

## Env

- Unity 2020.3.20 f1
- Azure.Messaging.WebPubSub 1.0.0

## Install & Usage

### Web PubSub リソースの作成

Azure Portal で Azure Web PubSub リソースを作成し、
リソースページのキーから接続文字列を確認します。
後で使用します。

### PubSub NuGet パッケージのインポート(Unity 起動前)

`./install.bat`を実行して必要な NuGet パッケージをインストールします。

### 接続文字列の設定

`Assets\PubSubAndroidTest\Settings\PubSubConfig.asset`に
Create メニューの PubSubTest>Config からコンフィグファイルを作成し、
接続文字列と適当な hub 名を入力します。default とかでいいと思います。

そのあと`./Assets/Scenes/SampleScene.unity`を開いて
PubSubTest オブジェクトにコンフィグファイルをアタッチします。

### シーンの再生

Play ボタンを押下すると接続が開始されます。

## Contact

何かございましたら[にー兄さん twitter](https://twitter.com/ninisan_drumath)までよろしくお願いします。
