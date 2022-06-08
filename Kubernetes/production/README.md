# create secrets
```
kubectl create secret generic db-user --from-literal=SA_PASSWORD="YOUR_SECRET_PASSWORD"
```
```
kubectl create secret generic db-advertisement --from-literal=SA_PASSWORD="YOUR_SECRET_PASSWORD"
```
```
kubectl create secret generic rabbitmq --from-literal=RABBITMQ_USERNAME="YOUR_SECRET_USERNAME" --from-literal=RABBITMQ_PASSWORD="YOUR_SECRET_PASSWORD"
```
# Create namespace
```
kubectl apply -f namespace.yaml
```
# Create storage class
```
kubectl apply -f storageclass.yaml
```
# Create volume claims
```
kubectl apply -f advertisement-pvc.yaml
```
```
kubectl apply -f user-pvc.yaml
```
# Create databases
```
kubectl apply -f advertisement-mssql-deployment.yaml
```
```
kubectl apply -f user-mssql-deployment.yaml
```
# Create service buss
```
kubectl apply -f rabbitmq-deployment.yaml
```
# Create gateway
```
kubectl apply -f gateway-deployment.yaml
```
# Create services
```
kubectl apply -f advertisement-deployment.yaml
```
```
kubectl apply -f user-deployment.yaml
```