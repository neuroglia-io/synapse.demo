﻿// Copyright © 2022-Present The Synapse Authors. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Synapse.Demo.WebUI;

public abstract class StatefulComponent<TState>
    : ComponentBase, IDisposable
{

    private bool _Disposed;
    private IDisposable? _Subscription;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = null!;

    [Inject]
    public IStore Store { get; set; } = null!;

    public IFeature<TState> Feature { get; private set; } = null!;

    public TState State => this.Feature.State;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.Feature = this.Store.GetFeature<TState>();
        this._Subscription = this.Feature.DistinctUntilChanged().Subscribe(_ => this.StateHasChanged());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._Disposed)
        {
            if (disposing)
            {
                this._Subscription?.Dispose();
                this._Subscription = null;
            }
            this._Disposed = true;
        }
    }

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

}

