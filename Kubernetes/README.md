# create secrets
```
kubectl create secret generic mssql --from-literal=SA_PASSWORD="Y0UR_S3CR3T_PA55W0RD!"
```
# pvc
```
kubectl apply -f pvc.yaml
```
# databases
```
kubectl apply -f mssql-advertisement-deployment.yaml
```
```
kubectl apply -f mssql-user-deployment.yaml
```
# Messagebus Rabbit-MQ
```
kubectl apply -f rabbitmq-deployment.yaml
```
# microservices
```
kubectl apply -f advertisement-deployment.yaml
```
```
kubectl apply -f user-deployment.yaml
```
# gateway
```
kubectl apply -f gateway-deployment.yaml
```