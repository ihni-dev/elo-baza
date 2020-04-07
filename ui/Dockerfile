FROM node:13.12.0-buster-slim as build
WORKDIR /app
COPY . .
RUN npm install && npm run build -- --prod

FROM nginx:1.17.9
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/ /usr/share/nginx/html
