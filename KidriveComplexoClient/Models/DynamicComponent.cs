using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.AspNetCore.Components
{
    public class DynamicComponent : IComponent
    {
        private RenderHandle _renderHandle;
        private RenderFragment _cachedRenderFragment;


        public DynamicComponent()
        {
            _cachedRenderFragment = Render;
        }

        [Parameter]
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public Type Type { get; set; } = default!;

        [Parameter]
        public IDictionary<string, object>
    ? Parameters
        { get; set; }

        /// <inheritdoc />
        public void Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
        }

        /// <inheritdoc />
        public Task SetParametersAsync(ParameterView parameters)
        {
            // This manual parameter assignment logic will be marginally faster than calling
            // ComponentProperties.SetProperties.
            foreach (var entry in parameters)
            {
                if (entry.Name.Equals(nameof(Type), StringComparison.OrdinalIgnoreCase))
                {
                    Type = (Type)entry.Value;
                }
                else if (entry.Name.Equals(nameof(Parameters), StringComparison.OrdinalIgnoreCase))
                {
                    Parameters = (IDictionary<string, object>
                        )entry.Value;
                }
                else
                {
                    throw new InvalidOperationException(
                    $"{nameof(DynamicComponent)} does not accept a parameter with the name '{entry.Name}'. To pass parameters to the dynamically-rendered component, use the '{nameof(Parameters)}' parameter.");
                }
            }

            if (Type is null)
            {
                throw new InvalidOperationException($"{nameof(DynamicComponent)} requires a non-null value for the parameter {nameof(Type)}.");
            }

            _renderHandle.Render(_cachedRenderFragment);
            return Task.CompletedTask;
        }

        void Render(RenderTreeBuilder builder)
        {
            builder.OpenComponent(0, Type);

            if (Parameters != null)
            {
                foreach (var entry in Parameters)
                {
                    builder.AddAttribute(1, entry.Key, entry.Value);
                }
            }

            builder.CloseComponent();
        }

    }
}
