FROM node:14.4.0-buster-slim as build
WORKDIR /app
COPY . .
RUN npm install && npm run build -- --prod

FROM nginx:1.19.0
COPY --from=build /app/dist/ /usr/share/nginx/html
COPY --from=build /app/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/tools/replace_api_url.sh /

CMD ["sh", "replace_api_url.sh"]