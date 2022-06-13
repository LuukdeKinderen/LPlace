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

# Create database services
```
kubectl apply -f advertisement-mssql-svc.yaml
```
```
kubectl apply -f user-mssql-svc.yaml
```
# Create database state full sets
```
kubectl apply -f advertisement-mssql-sfs.yaml
```
```
kubectl apply -f user-mssql-sfs.yaml
```

# Create service bus
```
kubectl apply -f rabbitmq-deployment.yaml
```

# Create services
```
kubectl apply -f advertisement-deployment.yaml
```
```
kubectl apply -f user-deployment.yaml
```

# Create gateway
```
kubectl apply -f gateway-deployment.yaml
```


# Monitoring
```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.5.0/aio/deploy/recommended.yaml
```
```
kubectl apply -f monitoring.yaml
```
```
kubeclt proxy
```
go to http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/ to see the dacshboard