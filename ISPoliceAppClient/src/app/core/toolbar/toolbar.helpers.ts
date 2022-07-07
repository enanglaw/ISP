export interface Notification {
  id: string;
  title: string;
  lastTime: string;
  state: string;
}
export interface CurrentUser {
  photoURL: string;
  currentUserName: string;
}
export const ToolbarHelpers: {
  notifications: Notification[];
  currentUser: CurrentUser;
} = {
  notifications: [
    {
      id: 'id',
      title: 'Mail 5',
      lastTime: '23 Minutes ago',
      state: 'state',
    },
    {
      id: 'id',
      title: 'Mail 5',
      lastTime: '23 Minutes ago',
      state: 'state',
    },
    {
      id: 'id',
      title: 'Mail 5',
      lastTime: '23 Minutes ago',
      state: 'state',
    },
  ],

  currentUser: {
    photoURL: 'assets/images/avatars/noavatar.png',
    currentUserName: 'Admin',
  },
};
