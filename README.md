# MA-Resonite-SharedObjectResolver

## これはなに？

[modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) を Linux 上で動作させるために、 resonite の engine が要求する Native DLL (.so) をかき集めるための存在です。

## 何故外部に？

[modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) は基本的に Windows のみサポートであり、必要な `.so` の増加や アップデートが必要になるたびに PR を送りリリースを待つのは非効率なため、責任領域の分割もあり、Linux に住む[私](https://github.com/ReinaS-64892)が別でメンテナーとなれる外部のパッケージになっています。

## かき集めるタイミング

`[InitializeOnLoadMethod]` のタイミングでかき集め非同期的に展開するので、神速で [modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) のビルドを実行すると失敗する可能性があります。(現実的に人間の速度であればかき集める速度のほうが早いので現実的には導入したら後は気にしなくてよいです。)

## VPM のこと

このレポジトリは [modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) の動向に大きく左右され、今後不要になる可能性すら存在します。なので現状は git clone して使用してください！

## Linuxでのビルド方法

現時点(このセクションが追加された 2025/05/18)では、 [modular-avatar-resonite](https://github.com/bdunderscore/modular-avatar-resonite) の VPM 配布パッケージには Linux 向けバイナリが追加されていません。

自身でビルドする必要があります。

### 必要なパッケージ

dotnet 9.0 と powershell が必要です。環境に応じたパッケージマネージャーを使用し適宜インストールしてください。

exsample(arch & aur helper is paru)

```bash
[Reina@RS-ArchMainPC ~]$ paru -S dotnet-sdk-9.0
[Reina@RS-ArchMainPC ~]$ paru -S powershell
```

### GitClone

`UnityProjectRoot/Package/modular-avatar-resonite` になるように `https://github.com/bdunderscore/modular-avatar-resonite` を `git clone` してください。

### Resonite への symbolic link

from `/modular-avatar-resonite/Resonite~/ResoniteDir`  
to `/home/${USER}/.local/share/Steam/steamapps/common/Resonite`

となるように symbolic link を作成してください

もし Resonite のインストール場所が違う場合はそれに応じて書き換えてください。

exsample

```bash
[Reina@RS-ArchMainPC Resonite~]$ pwd
/home/Reina/Rs/UnityProject/Lime-ReinaSEdit/Packages/modular-avatar-resonite/Resonite~
[Reina@RS-ArchMainPC Resonite~]$ ln -s /home/Reina/.local/share/Steam/steamapps/common/Resonite ResoniteDir
[Reina@RS-ArchMainPC Resonite~]$ ls
ResoniteDir  ResoniteHook
[Reina@RS-ArchMainPC Resonite~]$ ls ResoniteDir
Assimp.dll     Locale  Migrations        Resonite_Data  RuntimeData      SystemHelper.log  UnityCrashHandler64.exe  vkd3d-proton.cache
Build.version  Logs    MonoBleedingEdge  Resonite.exe   steam_appid.txt  Tools             UnityPlayer.dll
```

### Build

PowerShell を呼び出し `./DevTools~/SyncToUnity.ps1` を実行することで `Managed` や `ResoPuppet~` が生成され、 ModulerAvatar-Resonite の実行バイナリや Unity 用の DLL が生成されます。

```bash
[Reina@RS-ArchMainPC modular-avatar-resonite]$ pwd
/home/Reina/Rs/UnityProject/Lime-ReinaSEdit/Packages/modular-avatar-resonite
[Reina@RS-ArchMainPC modular-avatar-resonite]$ pwsh
PowerShell 7.5.1
PS /home/Reina/Rs/UnityProject/Lime-ReinaSEdit/Packages/modular-avatar-resonite> ./DevTools~/SyncToUnity.ps1
Restore complete (1.0s)

...(中略)...

Build succeeded with 93 warning(s) in 7.9s

    Directory: /home/Reina/Rs/UnityProject/Chocolat-ReinaSEdit/Packages/modular-avatar-resonite

UnixMode         User Group         LastWriteTime         Size Name
--------         ---- -----         -------------         ---- ----
drwxr-xr-x      Reina wheel       5/18/2025 23:38         4096 ResoPuppet~

PS /home/Reina/Rs/UnityProject/Lime-ReinaSEdit/Packages/modular-avatar-resonite> ls ./Managed
Google.Protobuf.dll  Grpc.Core.dll             ResoPuppetSchema.deps.json  ResoPuppetSchema.dll.config  System.Buffers.dll  System.Numerics.Vectors.dll
Grpc.Core.Api.dll    GrpcDotNetNamedPipes.dll  ResoPuppetSchema.dll        ResoPuppetSchema.pdb         System.Memory.dll   System.Runtime.CompilerServices.Unsafe.dll
PS /home/Reina/Rs/UnityProject/Lime-ReinaSEdit/Packages/modular-avatar-resonite> ls ./ResoPuppet~/
Google.Protobuf.dll         Grpc.Net.Common.dll        Launcher.dll                 PuppeteerCommon.dll  Puppeteer.pdb
Grpc.AspNetCore.Server.dll  JetBrains.Annotations.dll  Launcher.pdb                 PuppeteerCommon.pdb  Puppeteer.runtimeconfig.json
Grpc.Core.Api.dll           Launcher                   Launcher.runtimeconfig.json  Puppeteer.deps.json  System.CommandLine.dll
GrpcDotNetNamedPipes.dll    Launcher.deps.json         Puppeteer                    Puppeteer.dll
```

### このレポジトリを Package 配下にクローン

Linux で Resonite のエンジンを動かすために必須な `.so` を解決するためにこのレポジトリを `Packages/` にクローンします。

exsample

```bash
PS /home/Reina/Rs/UnityProject/Lime-ReinaSEdit/Packages/modular-avatar-resonite> exit
[Reina@RS-ArchMainPC modular-avatar-resonite]$ cd ..
[Reina@RS-ArchMainPC Packages]$ git clone https://github.com/ReinaS-64892/MA-Resonite-SharedObjectResolver.git
Cloning into 'MA-Resonite-SharedObjectResolver'...
remote: Enumerating objects: 23, done.
remote: Counting objects: 100% (23/23), done.
remote: Compressing objects: 100% (22/22), done.
remote: Total 23 (delta 5), reused 14 (delta 1), pack-reused 0 (from 0)
Receiving objects: 100% (23/23), 7.35 KiB | 7.35 MiB/s, done.
Resolving deltas: 100% (5/5), done.
[Reina@RS-ArchMainPC Packages]$ ls MA-Resonite-SharedObjectResolver
Editor  Editor.meta  LICENSE.md  LICENSE.md.meta  package.json  package.json.meta  README.md  README.md.meta
```

### 最後に

あとは UnityEditor の DomainReload を待った後に、通常の手順と同様に`実験的な機能`を有効化し`NDMF Console` からビルドを行いましょう。

[公式ドキュメント](https://modular-avatar.nadena.dev/dev/ja/docs/experimental-features/resonite-support)

### アップデートを反映する

`git pull` など、新しい commit を反映した後、 [Build](#build) のセクションの手順を行ってください。
