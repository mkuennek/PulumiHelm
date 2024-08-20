This repo shows differences when deploying a Helm chart using the Helm CLI and Pulumi when trying to set null values.

# Deploy using Pulumi
You need to have access to a Kubernetes cluster, e.g. using [kind](https://kind.sigs.k8s.io). The, run *pulumi up* and confirm.

After the OpenTelemetry Collector is deployed, inspect the created configmap by calling *kubectl describe cm otel-collector-opentelemetry-collector*. It will output something like:

```
Data
====
relay:
----
exporters:
  debug: {}
extensions:
  health_check:
    endpoint: ${env:MY_POD_IP}:13133
processors:
  batch: {}
  memory_limiter:
    check_interval: 5s
    limit_percentage: 80
    spike_limit_percentage: 25
receivers:
  jaeger:
    protocols:
      grpc:
        endpoint: ${env:MY_POD_IP}:14250
      thrift_compact:
        endpoint: ${env:MY_POD_IP}:6831
      thrift_http:
        endpoint: ${env:MY_POD_IP}:14268
  otlp:
    protocols:
      grpc:
        endpoint: ${env:MY_POD_IP}:4317
      http:
        endpoint: ${env:MY_POD_IP}:4318
  prometheus:
    config:
      scrape_configs:
      - job_name: opentelemetry-collector
        scrape_interval: 10s
        static_configs:
        - targets:
          - ${env:MY_POD_IP}:8888
  zipkin:
    endpoint: ${env:MY_POD_IP}:9411
service:
  extensions:
  - health_check
  pipelines:
    logs:
      exporters:
      - debug
      processors:
      - memory_limiter
      - batch
      receivers:
      - otlp
    metrics:
      exporters:
      - debug
      processors:
      - memory_limiter
      - batch
      receivers:
      - otlp
      - prometheus
    traces:
      exporters:
      - debug
      processors:
      - memory_limiter
      - batch
      receivers:
      - otlp
  telemetry:
    metrics:
      address: ${env:MY_POD_IP}:8888
```

Notice, that service->pipelines->traces is set

# Deploy using the Helm CLI
Destroy the pulumi stack. Add the OpenTelemetry Helm repo by calling *helm repo add open-telemetry https://open-telemetry.github.io/opentelemetry-helm-charts*. Then install using the following command *helm install -f OtelCollector.yaml otel-collector open-telemetry/opentelemetry-collector*.

When you now inspect the generated ConfigMap it will output something like:

```
Data
====
relay:
----
exporters:
  debug: {}
extensions:
  health_check:
    endpoint: ${env:MY_POD_IP}:13133
processors:
  batch: {}
  memory_limiter:
    check_interval: 5s
    limit_percentage: 80
    spike_limit_percentage: 25
receivers:
  otlp:
    protocols:
      grpc:
        endpoint: ${env:MY_POD_IP}:4317
      http:
        endpoint: ${env:MY_POD_IP}:4318
service:
  extensions:
  - health_check
  pipelines:
    traces:
      exporters:
      - debug
      processors:
      - memory_limiter
      - batch
      receivers:
      - otlp
  telemetry:
    metrics:
      address: ${env:MY_POD_IP}:8888

```

Notice that the metrics pipeline is not set. This is the wanted behavior I am unable to reproduce with Polumi.
