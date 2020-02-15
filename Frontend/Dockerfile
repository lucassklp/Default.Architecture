FROM node:12.16.0 as build

WORKDIR /src
COPY . .
RUN npm install -g @angular/cli@9.0.2
RUN npm install
RUN ng build --prod 

FROM nginx:1.17.8 as publish
COPY ./nginx/default.conf /etc/nginx/conf.d/
COPY --from=build /src/dist/default-architecture /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]