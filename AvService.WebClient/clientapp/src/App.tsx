import React, { Component } from 'react';
import * as signalR from "@microsoft/signalr";
import './App.css';

import * as AvService from '../../../AvService.Shared/AvService.Shared';
import { findDOMNode } from 'react-dom';

interface IState {
    hubConnection: signalR.HubConnection | null;
    notifications: [AvService.Notification, string][]
}

class App extends Component<{}, IState> {

    constructor(props: any) {
        super(props);
        this.state = { notifications: [], hubConnection: null };
    }

    render() {
        const listItems = this.state.notifications.map((item: [AvService.Notification, string]) => {
            return (<div key={Math.random().toString(36).substr(2, 9)}>{JSON.stringify(item)}</div>
            )
        });

        return (
            <div className="App">
                <div>
                    <button onClick={() => this.connect()}>Connect</button>
                    <button onClick={() => this.disconnect()}>Disconnect</button>

                    <button onClick={() => this.enableRealTimeScan()}>Enable Real Time Scan</button>
                    <button onClick={() => this.disableRealTimeScan()}>Disable Real Time Scan</button>

                    <button onClick={() => this.startOnDemandScan()}>Start On Demand Scan</button>
                    <button onClick={() => this.stopOnDemandScan()}>Stop On Demand Scan</button>

                    <button onClick={() => this.publishUnsentNotifications()}>Publish Unsent Notifications</button>
                </div>
                <div>{listItems}</div>
            </div>
        );
    }

    private connect = async () => {

        if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected ||
            this.state.hubConnection?.state === signalR.HubConnectionState.Connecting)
            return;

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5000/scanhub")
            .build();

        connection.on('SendStartScanOnDemandNotification', (notification) => {
            this.receiveNotification(notification, 'SendStartScanOnDemandNotification');
        });

        connection.on('SendStopScanOnDemandNotification', (notification) => {
            this.receiveNotification(notification, 'SendStopScanOnDemandNotification');
        });

        connection.on('SendStopScanSuccessNotification', (notification) => {
            this.receiveNotification(notification, 'SendStopScanSuccessNotification');
        });

        connection.on('SendThreatFoundNotification', (notification) => {
            this.receiveNotification(notification, 'SendThreatFoundNotification');
        });

        connection.on('SendScanInProgressNotification', (notification) => {
            this.receiveNotification(notification, 'SendScanInProgressNotification');
        });

        connection.on('DisconnectClient', async () => {
            try {
                if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected) {
                    console.log("stopping connection");
                    await new Promise(resolve => setTimeout(resolve, 100));
                    await this.state.hubConnection?.stop();
                    console.log("connection stopped");
                }
            }
            catch (e) { }
            finally {
                this.setState({ notifications: this.state.notifications, hubConnection: null });
            }
        });

        this.setState({
            hubConnection: connection,
            notifications: []
        });

        await connection.start();

        if (connection.state === signalR.HubConnectionState.Connected)
            await connection.invoke("Connect");
    }

    private disconnect = async () => {
        if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
            await this.state.hubConnection?.invoke("Disconnect");
    }

    private disableRealTimeScan = async () => {
        if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
            await this.state.hubConnection.invoke("DisableRealTimeScan");
    }

    private enableRealTimeScan = async () => {
        if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
            await this.state.hubConnection.invoke("EnableRealTimeScan");
    }

    private startOnDemandScan = async () => {
        if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
            await this.state.hubConnection.invoke("StartOnDemandScan");
    }

    private stopOnDemandScan = async () => {
        if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
            await this.state.hubConnection.invoke("StopOnDemandScan");
    }

    private publishUnsentNotifications = async () => {
        if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
            await this.state.hubConnection.invoke("PublishUnsentNotifications");
    }

    private receiveNotification = (notification: AvService.Notification, notificationType: string) => {
        console.log(notification);
        var updatedNotifications = this.state.notifications.concat([notification, notificationType]);
        this.setState({ notifications: updatedNotifications, hubConnection: this.state.hubConnection });
    }
}

export default App;
