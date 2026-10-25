@echo off

echo ============================
echo Starting Reading Diary...
echo ============================

docker compose up -d --build

echo.
echo ============================
echo Containers started
echo ============================
echo Web: http://localhost:5080
echo API: http://localhost:7093
echo.

pause