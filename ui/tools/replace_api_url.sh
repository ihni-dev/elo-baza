find '/usr/share/nginx/html/assets' -name 'app-configuration.json' -exec sed -i -e 's,"apiUrl": .*","apiUrl": "'"$API_URL"'",g' {} \;
cat /usr/share/nginx/html/assets/app-configuration.json
nginx -g "daemon off;"