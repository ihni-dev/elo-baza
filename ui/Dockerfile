FROM node:14.4.0-buster-slim as build
WORKDIR /app
COPY . .
RUN npm install && npm run build -- --prod

FROM nginx:1.19.0
COPY --from=build /app/dist/ /usr/share/nginx/html
