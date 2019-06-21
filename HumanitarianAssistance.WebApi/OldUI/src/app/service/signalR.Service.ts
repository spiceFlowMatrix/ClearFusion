import { NgZone, Injectable, OnInit } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

// tslint:disable-next-line:no-any
declare var $: any;

export class User {
  public UserId: string;
  public Email: string;
  public Name: string;
  public Photo: string;
  public GroupName: string;
  public IsMember: boolean;
}

export enum ConnectionState {
  Connecting = 1,
  Connected = 2,
  Reconnecting = 3,
  Disconnected = 4
}
@Injectable()
export class SignalRService implements OnInit {

//   messageData: MessageData = { ID: '', status: '', type: '' };
  /**
   * starting$ is an observable available to know if the signalr
   * connection is ready or not. On a successful connection this
   * stream will emit a value.
   */
  // tslint:disable-next-line:no-any
  starting$: Observable<any>;

  /**
   * connectionState$ provides the current state of the underlying
   * connection as an observable stream.
   */
  connectionState$: Observable<ConnectionState>;

  /**
   * error$ provides a stream of any error messages that occur on the
   * SignalR connection
   */
  error$: Observable<string>;

  joinGroupEvent$: Observable<User>;
  notifyEvent$: Observable<User>;
  notifyNewMembersEvent$: Observable<User>;
//   messageReceivedEvent$: Observable<MessageData>;
//   oldMessagesEvent$: Observable<Array<MessageData>>;
  leaveGroupEvent$: Observable<User>;
  membersStatusEvent$: Observable<User>;
//   deleteMessageEvent$: Observable<MessageData>;

  // These are used to feed the public observables
  private connectionStateSubject = new Subject<ConnectionState>();
  // tslint:disable-next-line:no-any
  private startingSubject = new Subject<any>();
  // tslint:disable-next-line:no-any
  private errorSubject = new Subject<any>();

  private joinGroupSubject: Subject<User> = new Subject<User>();
  private notifySubject: Subject<User> = new Subject<User>();
  private notifyNewSubject: Subject<User> = new Subject<User>();
//   private messageReceivedSubject: Subject<MessageData> = new Subject<MessageData>();
//   private oldMessagesSubject: Subject<Array<MessageData>> = new Subject<Array<MessageData>>();
  private leaveGroupSubject: Subject<User> = new Subject<User>();
  private membersStatusSubject: Subject<User> = new Subject<User>();
//   private deleteMessageSubject: Subject<MessageData> = new Subject<MessageData>();

  // tslint:disable-next-line:no-any
  private hub: any;

  // tslint:disable-next-line:no-any
  private hubName = 'chaHub';

  // tslint:disable-next-line:no-any
  private connection: any;

  constructor(private zone: NgZone) {
    // Set up our observables
    this.connectionState$ = this.connectionStateSubject.asObservable();
    this.starting$ = this.startingSubject.asObservable();
    this.error$ = this.errorSubject.asObservable();

    this.joinGroupEvent$ = this.joinGroupSubject.asObservable();
    this.notifyEvent$ = this.notifySubject.asObservable();
    this.notifyNewMembersEvent$ = this.notifyNewSubject.asObservable();
    // this.messageReceivedEvent$ = this.messageReceivedSubject.asObservable();
    // this.oldMessagesEvent$ = this.oldMessagesSubject.asObservable();
    this.leaveGroupEvent$ = this.leaveGroupSubject.asObservable();
    this.membersStatusEvent$ = this.membersStatusSubject.asObservable();
    // this.deleteMessageEvent$ = this.deleteMessageSubject.asObservable();
    // Configure Hub Connection
    this.setupHub();
    this.GroupEvents();
    this.NotifyMembers();
    this.RecievedMessage();
   this.startConnection();
    this.DeleteMessage();
  }
  ngOnInit(): void {


  }
  setupHub() {
    // create hub connection
    this.connection = $.hubConnection('http://localhost:5000');
    // this.connection = $.hubConnection('http://qctst.servcorpcommunity.com');
    // create new proxy as name already given in top

    this.hub = this.connection.createHubProxy(this.hubName);
    // this.hub.connection.url = 'http://localhost:5000/loopy';
    //   this.hub.on('Send', (data: any) => {
    //         // this.toastr.success(data);
    //           const received = `Received: ${data}`;
    //         //   this.messages.push(received);
    //       });
    // Define handlers for the connection state event
    this.connection.stateChanged((state) => {
      let newState = ConnectionState.Connecting;

      switch (state.newState) {
        case $.signalR.connectionState.connecting:
          newState = ConnectionState.Connecting;
          break;
        case $.signalR.connectionState.connected:
          newState = ConnectionState.Connected;
          break;
        case $.signalR.connectionState.reconnecting:
          newState = ConnectionState.Reconnecting;
          break;
        case $.signalR.connectionState.disconnected:
          newState = ConnectionState.Disconnected;
          break;
      }

      // Add ngZone to make UI apply the async data
      this.zone.run(() => {
        // Push the new state on our subject
        this.connectionStateSubject.next(newState);
      });


    });
    // Define handlers for any errors
    this.connection.error((error) => {
      this.zone.run(() => {
        // Push the error on our subject
        this.errorSubject.next(error);
      });
    });
  }
  public startConnection(): void {
    this.connection.start({ withCredentials: false }).done((data: any) => {
        // this.connectionEstablished.emit(true);
        // this.connectionExists = true;
    }).fail((error: any) => {
    });
    // this.connection.start().done((data) => {
    //   this.zone.run(() => {
    //     this.startingSubject.next(data.id);
    //   });

    // }).fail((error) => {
    //   this.zone.run(() => {
    //     this.startingSubject.error(error);
    //   });
    // });
  }

  public stopConnection(): void {
    this.connection.stop();
  }

  private GroupEvents(): void {
    this.hub.on('onGroupAccepted', (data: User) => {
      this.zone.run(() => {
        this.joinGroupSubject.next(data);
      });
    });

    this.hub.on('onGroupLeave', (data: User) => {
      this.zone.run(() => {
        this.leaveGroupSubject.next(data);
      });
    });
  }

  private NotifyMembers(): void {
    this.hub.on('notifyMembers', (data: User) => {
      this.zone.run(() => {
        this.notifySubject.next(data);
      });
    });

    this.hub.on('notifyNewMembers', (data: User) => {
      this.zone.run(() => {
        this.notifyNewSubject.next(data);
      });
    });

    // this.hub.on('loadOldMessages', (data: Array<MessageData>) => {
    //   this.zone.run(() => {
    //     this.oldMessagesSubject.next(data);
    //   });
    // });
  }

  private RecievedMessage(): void {
    this.hub.on('sendPrivateMessage', (userId, message, status, type) => {
    //   this.messageData.ID = userId;
    //   this.messageData.Message = message;
    //   this.messageData.status = status;
    //   this.messageData.type = type;
    //   this.messageReceivedSubject.next(this.messageData);
    });
  }
  private DeleteMessage(): void {
    this.hub.on('deletePrivateMessage', (message) => {
    //   this.deleteMessageSubject.next(message);
    });
  }

  public joinGroup() {
    // this.hub.invoke('connect', this.commonservice.getCurrentUserDetails().Email, this.commonservice.getCurrentUserDetails().Email,
    //   this.commonservice.getUserId());
  }

//   public notifyNewMembers(user: User, oldMessages: Array<MessageData>) {
//     this.hub.invoke('NotifyMembers', user);
//     if (oldMessages.length > 0) {
//       this.hub.invoke('OldMessages', oldMessages);
//     }
//   }

//   public sendMessage(message: MessageData) {
//     this.hub.invoke('SendMessage', message);
//   }

  public leaveGroup(user: User) {
    this.hub.invoke('LeaveGroup', user);
  }
  public sendPrivateMessageForServer(toUserId, message, type) {
    this.hub.invoke('sendPrivateMessage', toUserId, message, type);
  }
  public deletePrivateMessage(message, type) {
    this.hub.invoke('deletePrivateMessage', message, type);
  }
}
