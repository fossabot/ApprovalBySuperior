#ビルドする時はpublishフォルダにcdすること。
# cd /Users/sim/Documents/Projects/ApprovalBySuperior/ApprovalBySuperior/bin/Release/netcoreapp2.1/publish

# .NET Core 2.1 をベースイメージとする
FROM microsoft/dotnet
# 作業ディレクリの指定
WORKDIR /app
COPY . /app

# 一旦docker-composeを使わない場合は以下定義。個別にビルド
# docker build -t asia.gcr.io/<PROJECT_ID>/apprval_sim:v1 .
CMD ASPNETCORE_URLS=http://*:8080 dotnet bin/Release/netcoreapp2.1/publish/ApprovalBySuperior.dll
