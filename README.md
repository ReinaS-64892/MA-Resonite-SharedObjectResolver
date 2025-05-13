# MA-Resonite-SharedObjectResolver

## これはなに？

[modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) を Linux 上で動作させるために、 resonite の engine が要求する Native DLL (.so) をかき集めるための存在です。

## 何故外部に？

[modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) は基本的に Windows のみサポートであり、必要な `.so` の増加や アップデートが必要になるたびに PR を送りリリースを待つのは非効率なため、責任領域の分割もあり、Linux に住む人が別でメンテナーとなれる外部のパッケージになっています。

## かき集めるタイミング

`[InitializeOnLoadMethod]` のタイミングでかき集め非同期的に展開するので、神速で [modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) のビルドを実行すると失敗する可能性があります。(現実的に人間の速度であればかき集める速度のほうが早いので現実的には導入したら後は気にしなくてよいです。)

## VPM のこと

このレポジトリは [modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) の動向に大きく左右され、今後不要になる可能性すら存在します。なので現状は git clone して使用してください！
