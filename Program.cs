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
            ValueYamlFiles = new FileAsset("./OtelCollector.yaml"),
            // Values = new InputMap<object>
            // {
            //     ["mode"] = "deployment",
            //     ["image"] = new InputMap<object>
            //     {
            //         ["repository"] = "otel/opentelemetry-collector-contrib",
            //     },
            //     ["config"] = new InputMap<object>
            //     {
            //         ["receivers"] = new InputMap<object>
            //         {
            //             ["jaeger"] = new InputMap<object>(),
            //             ["prometheus"] = new InputMap<object>(),
            //             ["zipkin"] = new InputMap<object>(),
            //         },
            //         ["service"] = new InputMap<object>
            //         {
            //             ["pipelines"] = new InputMap<object>
            //             {
            //                 ["logs"] = new InputMap<object>(),
            //                 ["metrics"] = new InputMap<object>(),
            //                 ["traces"] = new InputMap<object>
            //                 {
            //                     ["receivers"] = new InputList<string>{ "otlp" },
            //                     ["exporters"] = new InputList<string>{ "debug" },
            //                 },
            //
            //             },
            //         }
            //     }
            //     ["debug"] = new InputMap<object>
            //     {
            //         ["jaeger-compact"] = new InputMap<object>
            //         {
            //             ["enabled"] = false,
            //         },
            //         ["jaeger-thrift"] = new InputMap<object>
            //         {
            //             ["enabled"] = false,
            //         },
            //         ["jaeger-grpc"] = new InputMap<object>
            //         {
            //             ["enabled"] = false,
            //         },
            //         ["zipkin"] = new InputMap<object>
            //         {
            //             ["enabled"] = false,
            //         },
            //     }
            // },
            AllowNullValues = true,
        });

    return new Dictionary<string, object?>();
});
