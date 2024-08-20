using System.Collections.Generic;
using Pulumi;
using Pulumi.Kubernetes.Helm.V3;
using Pulumi.Kubernetes.Types.Inputs.Helm.V3;

return await Deployment.RunAsync(() =>
{
    _ = new Release(
        "otel-collector", new ReleaseArgs
        {
            Name = "otel-collector",
            Chart = "opentelemetry-collector",
            Version = "0.97.1",
            RepositoryOpts = new RepositoryOptsArgs
            {
                Repo = "https://open-telemetry.github.io/opentelemetry-helm-charts"
            },
            ValueYamlFiles = new FileAsset("./OtelCollector.yaml")
        });

    return new Dictionary<string, object?>();
});
