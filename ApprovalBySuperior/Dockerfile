# 事前にVSでビルド（公開）でバイナリをパブリッシュしておく
# パブリッシュしたら、approvalとうディレクトリを作成して、そこにpublishフォルダを格納する。
#ビルドする時はpublishフォルダにcdすること。
# ビルドコマンドはこれ。
# sudo docker build ./ -t aprvsupe/aspnetcore:1.0

# Microsoft製の.NETCOREをベースイメージとする
# https://hub.docker.com/_/microsoft-dotnet-aspnet/
FROM mcr.microsoft.com/dotnet/aspnet:6.0
# 作業ディレクリの指定
WORKDIR /app
COPY . /app

CMD ASPNETCORE_URLS=http://*:8080 dotnet publish/ApprovalBySuperior.dll


#以下は.netcore2.1時代のもの
#ビルドする時はpublishフォルダにcdすること。
# cd /Users/sim/Documents/Projects/ApprovalBySuperior/ApprovalBySuperior/bin/Release/netcoreapp2.1/publish
# .NET Core 2.1 をベースイメージとする
#FROM microsoft/dotnet
# 作業ディレクリの指定
#WORKDIR /app
#COPY . /app
# 一旦docker-composeを使わない場合は以下定義。個別にビルド
# docker build -t asia.gcr.io/<PROJECT_ID>/apprval_sim:v1 .
#CMD ASPNETCORE_URLS=http://*:8080 dotnet bin/Release/netcoreapp2.1/publish/ApprovalBySuperior.dll
