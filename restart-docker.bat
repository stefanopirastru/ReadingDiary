@echo off

echo ============================
echo Restarting Reading Diary...
echo ============================

docker compose down

docker compose up -d --build

echo.
echo Restart completed
echo Web: http://localhost:5080
echo API: http://localhost:7093
echo.

pause