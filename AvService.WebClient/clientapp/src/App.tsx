import React, { Component } from 'react';
import * as signalR from "@microsoft/signalr";
import './App.css';

import * as AvService from '../../../AvService.Shared/AvService.Shared';

interface IState {
  hubConnection: signalR.HubConnection | null;
  notifications: AvService.Notification[]
}

class App extends Component<{}, IState> {

  constructor(props:any) {
    super(props);
    this.state = { notifications: [], hubConnection: null };
  }

  render() {
    const listItems = this.state.notifications.map((item: AvService.Notification) => {
      return (<div>{item.NotificationTime}</div>
      )
    });

    return (
      <div className="App">
        <div>{listItems}</div>
        <div>
          <button onClick={() => this.connect()}>Connect</button>
          <button onClick={() => this.disconnect()}>Disconnect</button>

          <button onClick={() => this.enableRealTimeScan()}>Enable Real Time Scan</button>
          <button onClick={() => this.disableRealTimeScan()}>Disable Real Time Scan</button>

          <button onClick={() => this.startOnDemandScan()}>Start On Demand Scan</button>
          <button onClick={() => this.stopOnDemandScan()}>Stop On Demand Scan</button>

          <button onClick={() => this.publishUnsentNotifications()}>Publish Unsent Notifications</button>

        </div>
      </div>
    );
  }

  componentDidMount = () => {
    console.log('componentDidMount');

    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/scanhub")
      .build();

    connection.on('SendStartScanOnDemandNotification', (notification) => {
      this.receiveNotification(notification);
    });

    connection.on('SendStopScanOnDemandNotification', (notification) => {
      this.receiveNotification(notification);
    });

    connection.on('SendStopScanSuccessNotification', (notification) => {
      this.receiveNotification(notification);
    });

    connection.on('SendThreatFoundNotification', (notification) => {
      this.receiveNotification(notification);
    });

    connection.on('SendScanInProgressNotification', (notification) => {
      this.receiveNotification(notification);
    });

    connection.on('DisconnectClient', () => {
      this.state.hubConnection?.stop();
    });

    this.setState({
      hubConnection: connection,
      notifications: []
    });
  }

  private connect = async () => {
    await this.state.hubConnection?.start().catch(err => console.log(err));
    await this.state.hubConnection?.invoke("Connect");
  }

  private disconnect = () => {
    if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
      this.state.hubConnection?.invoke("Disconnect");
  }

  private disableRealTimeScan = () => {
    if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
      this.state.hubConnection.invoke("DisableRealTimeScan");
  }

  private enableRealTimeScan = () => {
    if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
      this.state.hubConnection.invoke("EnableRealTimeScan");
  }

  private startOnDemandScan = () => {
    if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
      this.state.hubConnection.invoke("StartOnDemandScan");
  }

  private stopOnDemandScan = () => {
    if (this.state.hubConnection?.state === signalR.HubConnectionState.Connected)
      this.state.hubConnection.invoke("StopOnDemandScan");
  }

  private publishUnsentNotifications = () => {
    if (this.state.hubConnection?.state == signalR.HubConnectionState.Connected)
      this.state.hubConnection.invoke("PublishUnsentNotifications");
  }

  private receiveNotification = (notification: any) => {
    console.log(notification);
  }
}

export default App;
