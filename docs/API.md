# HTTP API

После запуска API доступно по адресу `http://127.0.0.1:5080/api`.

| Метод | Маршрут | Назначение |
|---|---|---|
| GET | `/api/health` | состояние и версия |
| GET | `/api/catalog/materials` | каталог материалов |
| GET | `/api/catalog/sections` | каталог сечений |
| POST | `/api/beam` | полный расчёт балки |
| POST | `/api/beam/select` | подбор IPE |
| POST | `/api/beam/report` | автономный HTML-отчёт |
| POST | `/api/column` | проверка колонны |
| GET | `/api/truss/demo` | демонстрационная ферма |

```bash
curl -s http://127.0.0.1:5080/api/beam \
  -H 'Content-Type: application/json' \
  --data @samples/beam.json
```

Локальный сервис не содержит аутентификации. Для сетевой публикации ограничьте доступ, размер запроса и число `meshElements`.
