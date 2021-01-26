using Core.UI;
using UnityEngine;
using LeanCloud;
using LeanCloud.Storage;

namespace TowerDefense.UI.HUD
{
	/// <summary>
	/// Main menu implementation for tower defense
	/// </summary>
	public class TowerDefenseMainMenu : MainMenu
	{
		/// <summary>
		/// Reference to options menu
		/// </summary>
		public OptionsMenu optionsMenu;
		
		/// <summary>
		/// Reference to title menu
		/// </summary>
		public SimpleMainMenuPage titleMenu;
		
		/// <summary>
		/// Reference to level select menu
		/// </summary>
		public LevelSelectScreen levelSelectMenu;

		/// <summary>
        /// Reference to login panel
        /// </summary>
		public LoginPanel loginPanel;

		/// <summary>
        /// Reference to register panel
        /// </summary>
		public RegisterPanel registerPanel;

		/// <summary>
        /// Reference to name panel
        /// </summary>
		public NamePanel namePanel;

		/// <summary>
        /// Reference to profile panel
        /// </summary>
		public ProfilePanel profilePanel;

		/// <summary>
        /// Reference to leaderboard panel
        /// </summary>
		public LeaderboardPanel leaderboardPanel;

		/// <summary>
        /// Reference to chat panel
        /// </summary>
		public WorldChatPanel worldChatPanel;

		/// <summary>
        /// Reference to friend panel
        /// </summary>
		public FriendPanel friendPanel;

		/// <summary>
        /// Reference to private chat panel
        /// </summary>
		public PrivateChatPanel privateChatPanel;

		/// <summary>
		/// Bring up the options menu
		/// </summary>
		public void ShowOptionsMenu()
		{
			ChangePage(optionsMenu);
		}
		
		/// <summary>
		/// Bring up the options menu
		/// </summary>
		public void ShowLevelSelectMenu()
		{
			ChangePage(levelSelectMenu);
		}

		/// <summary>
        /// Bring up to login menu
        /// </summary>
		public async void ShowLoginMenu() {
			if (LCUtils.TryGetLocalSessionToken(out string sessionToken)) {
				try {
					LCUser user = await LCManager.Instance.Login(sessionToken);
					string nickname = user.GetNickname();
					if (string.IsNullOrEmpty(nickname)) {
						ShowNameMenu();
					} else {
						ShowProfileMenu();
					}
				} catch (LCException e) {
					Debug.LogError($"{e.Code} - {e.Message}");
					ChangePage(loginPanel);
				}
			} else {
				ChangePage(loginPanel);
			}
        }

		/// <summary>
        /// Bring up to register menu
        /// </summary>
		public void ShowRegisterMenu() {
			ChangePage(registerPanel);
		}

		/// <summary>
        /// Bring up to name menu
        /// </summary>
		public void ShowNameMenu() {
			ChangePage(namePanel);
		}

		/// <summary>
        /// Bring up to profile menu
        /// </summary>
		public void ShowProfileMenu() {
			ChangePage(profilePanel);
		}

		/// <summary>
        /// Bring up to leaderboard menu
        /// </summary>
		public void ShowLeaderboard() {
			ChangePage(leaderboardPanel);
		}

		/// <summary>
        /// Bring up to chat menu
        /// </summary>
		public void ShowWorldChat() {
			ChangePage(worldChatPanel);
		}

		/// <summary>
        /// Bring up to friend menu
        /// </summary>
		public void ShowFriend() {
			ChangePage(friendPanel);
		}

		/// <summary>
        /// Bring up to private chat menu
        /// </summary>
		public void ShowPrivateChat(LCUser user) {
			privateChatPanel.target = user;
			ChangePage(privateChatPanel);
		}

		/// <summary>
		/// Returns to the title screen
		/// </summary>
		public void ShowTitleScreen()
		{
			Back(titleMenu);
		}

		/// <summary>
		/// Set initial page
		/// </summary>
		protected virtual void Awake()
		{
			ShowTitleScreen();
		}

		/// <summary>
		/// Escape key input
		/// </summary>
		protected virtual void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				if ((SimpleMainMenuPage)m_CurrentPage == titleMenu)
				{
					Application.Quit();
				}
				else
				{
					Back();
				}
			}
		}
	}
}