/*
	when logged in, user can create projects,
	user that creates project is set as admin role (roles table must be created beforehand)
	(3 table connect user, role, project_user - user has one role per project)
	on project create admin user in roles, and a link of user_project (user_id, role_id, project_id)

*/
use testing;

drop database if exists task_manager;
create database task_manager;
use task_manager;

drop table if exists STATUS;
create table STATUS(
	[ID] int identity(1,1) not null,
	[TAG] nvarchar(50),
	[VALUE] nvarchar(200),
	constraint PK_STATUS primary key(ID),
);

-- IE URL char limit
drop table if exists PROJECTS;
create table PROJECTS (
	[ID] int identity(1,1) not null,
	[TITLE] nvarchar(200) not null,
	[KEY] nvarchar(32) not null,
	[DESCRIPTION] nvarchar(1000),
	[PICTURE_URL] nvarchar(2083) null,
	[STATUS_ID] int not null default 1,
	[CREATED_DATE] datetime default CURRENT_TIMESTAMP,
	constraint PK_PROJECTS primary key(ID),
	constraint FK_PROJECTS_STATUS foreign key (STATUS_ID) references STATUS(ID)
);

drop table if exists PROJECT_ROLES;
create table PROJECT_ROLES (
	[ID] int IDENTITY(1,1) not null,
	[ROLE_NAME] nvarchar(200) not null,
	[ROLE_DESCRIPTION] nvarchar(400) null,
	constraint PK_PROJECT_ROLES primary key(ID)
);

drop table if exists CATEGORIES;
create table CATEGORIES(
	[ID] int IDENTITY(1,1) not null,
	[VALUE] nvarchar(100) not null,
	[DESCRIPTION] nvarchar(1000),
	constraint PK_CATEGORIES primary key(ID)
);

drop table if exists PROJECT_CATEGORIES;
create table PROJECT_CATEGORIES(
	[ID] int IDENTITY(1,1) not null,
	[CATEGORY_ID] int not null,
	[PROJECT_ID] int not null,
	constraint PK_PROJECT_CATEGORIES primary key(ID),
	constraint FK_PROJECT_CATEGORIES_CATEGORY foreign key(CATEGORY_ID) references CATEGORIES(ID),
	constraint FK_PROJECT_CATEGORIES_PROJECTS foreign key(PROJECT_ID) references PROJECTS(ID)
);

/*
drop table if exists PROJECT_CLAIMS;
create table PROJECT_CLAIMS(
	[ID] int IDENTITY(1,1) not null,
	[CLAIM] nvarchar(300),
	constraint PK_PROJECT_CLAIMS primary key(ID)
);

drop table if exists ROLE_CLAIMS;
create table ROLE_CLAIMS(
	[ID] int IDENTITY(1,1) not null,
	[ROLE_ID] int not null,
	[CLAIM_ID] int foreign key references PROJECT_CLAIMS(ID),
	constraint PK_ROLE_CLAIMS primary key(ID),
	constraint FK_ROLE_CLAIMS_PROJECT_ROLES foreign key (ROLE_ID) references PROJECT_ROLES(ID),
	constraint FK_ROLE_CLAIMS_PROJECT_CLAIMS foreign key (CLAIM_ID) references PROJECT_CLAIMS(ID)
);
*/
drop table if exists USER_PROJECTS;
create table USER_PROJECTS (
	[ID] int identity(1,1) not null,
	[USER_ID] nvarchar(450) not null,
	[PROJECT_ID] int not null,
	[IS_VERIFIED] bit not null default 0,
	[TOKEN] varchar(37),
	[ROLE_ID] int not null,
	[IS_FAVORITE] bit not null default 0,
	constraint PK_USER_PROJECTS primary key(ID),
	constraint FK_USER_PROJECTS_PROJECTS foreign key (PROJECT_ID) references PROJECTS(ID),
	constraint FK_USER_PROJECTS_USERS foreign key (USER_ID) references AspNetUsers(ID),
	constraint FK_USER_PROJECTS_PROJECT_ROLES foreign key (ROLE_ID) references PROJECT_ROLES(ID)
);

/*
drop table if exists PROJECT_USER_CLAIMS;
create table PROJECT_USER_CLAIMS(
	[USER_PROJECTS_ID] int identity(1,1) not null,
	[CLAIM_ID] int not null,
	constraint PK_PROJECT_USER_CLAIMS primary key(USER_PROJECTS_ID, CLAIM_ID),
	constraint FK_PROJECT_USER_CLAIMS_PROJECT_CLAIMS foreign key (CLAIM_ID) references PROJECT_CLAIMS(ID),
	constraint FK_PROJECT_USER_CLAIMS_USER_PROJECTS foreign key (USER_PROJECTS_ID) references USER_PROJECTS(ID)
);
*/

drop table if exists STATES;
create table STATES(
	[ID] int identity(1,1) not null,
	[TAG] nvarchar(50),
	[VALUE] nvarchar(200),
	constraint PK_STATES primary key(ID),
);

drop table if exists PRIORITIES;
create table PRIORITIES(
	[ID] int identity(1,1) not null,
	[TAG] nvarchar(50),
	[VALUE] nvarchar(200),
	constraint PK_PRIORITIES primary key(ID),
);

drop table if exists TASKS;
create table TASKS (
	[ID] int identity(1,1) not null,
	[ISSUER_ID] nvarchar(450) not null,
	[SUMMARY] nvarchar(120) null,
	[DESCRIPTION] nvarchar(1000),
	[PROJECT_ID] int not null,
	[PICTURE_URL] nvarchar(2083) null, -- IE URL char limit
	[DEADLINE] datetime null,
	[CREATED_DATE] datetime not null default CURRENT_TIMESTAMP,
	[LAST_CHANGE_DATE] datetime,
	[PRIORITY_ID] int not null default 1,
	[STATE_ID] int not null default 1,
	[STATUS_ID] int not null default 1,
	constraint PK_TASKS primary key(ID),
	constraint FK_TASK_PROJECTS foreign key (PROJECT_ID) references PROJECTS(ID),
	constraint FK_TASKS_USERS foreign key (ISSUER_ID) references AspNetUsers(ID),
	constraint FK_TASKS_STATUS foreign key (STATUS_ID) references STATUS(ID),
	constraint FK_TASKS_STATES foreign key (STATE_ID) references STATES(ID),
	constraint FK_TASKS_PRIORITIES foreign key (PRIORITY_ID) references PRIORITIES(ID)
);

drop table if exists USER_TASKS;
create table USER_TASKS (
	[ID] int IDENTITY(1,1) not null,
	[USER_ID] nvarchar(450) not null,
	[TASK_ID] int not null,
	[ASSIGN_DATE] datetime not null default CURRENT_TIMESTAMP,
	constraint PK_USER_TASKS primary key(USER_ID,TASK_ID),
	constraint FK_USER_TASKS_TASKS foreign key (TASK_ID) references TASKS(ID),
	constraint FK_USER_TASKS_USERS foreign key (USER_ID) references AspNetUsers(ID)
);

drop table if exists ATTACHMENTS;
create table ATTACHMENTS(
	[ID] int IDENTITY(1,1) not null,
	[FILEPATH] nvarchar(260) default null,
	constraint PK_ATTACHMENTS primary key(ID)
);

drop table if exists TASK_ATTACHMENTS;
create table TASK_ATTACHMENTS (
	[ID] int IDENTITY(1,1) not null,
	[TASK_ID] int not null,
	[ATTACHMENT_ID] int not null,
	constraint PK_TASK_ATTACHMENTS primary key(ID),
	constraint FK_TASK_ATTACHMENTS_TASKS foreign key (TASK_ID) references TASKS(ID),
	constraint FK_TASK_ATTACHMENTS_ATTACHMENTS foreign key (ATTACHMENT_ID) references ATTACHMENTS(ID)
);

drop table if exists ISSUES;
create table ISSUES (
	[ID] int IDENTITY(1,1) not null,
	[TITLE] nvarchar(200) not null,
	[DESCRIPTION] nvarchar(1000),
	[CREATED_DATE] datetime not null default CURRENT_TIMESTAMP,
	--[LAST_CHANGE_DATE] datetime,
	[SOLVED_DATE] datetime,
	[ISSUER_ID] nvarchar(450) not null,
	[PROJECT_ID] int not null,
	constraint PK_ISSUES primary key(ID),
	constraint FK_ISSUES_PROJECTS foreign key (PROJECT_ID) references PROJECTS(ID),
	constraint FK_ISSUES_USERS foreign key (ISSUER_ID) references AspNetUsers(ID)
);

drop table if exists ISSUE_ATTACHMENTS;
create table ISSUE_ATTACHMENTS (
	[ID] int IDENTITY(1,1) not null,
	[ISSUE_ID] int not null,
	[ATTACHMENT_ID] int not null,
	constraint PK_ISSUE_ATTACHMENTS primary key(ID),
	constraint FK_ISSUE_ATTACHMENTS_ISSUES foreign key (ISSUE_ID) references ISSUES(ID),
	constraint FK_ISSUE_ATTACHMENTS_ATTACHMENTS foreign key (ATTACHMENT_ID) references ATTACHMENTS(ID)
);

drop table if exists TASK_COMMENTS;
create table TASK_COMMENTS (
	[ID] int IDENTITY(1,1) not null,
	[TEXT] text null,
	[USER_ID] nvarchar(450) not null,
	[TASK_ID] int not null,
	[DATE_CREATED] datetime default CURRENT_TIMESTAMP,
	[LAST_CHANGE_DATE] datetime default null,
	constraint PK_TASK_COMMENTS primary key(ID),
	constraint FK_TASK_COMMENTS_TASKS foreign key (TASK_ID) references TASKS(ID),
	constraint FK_TASK_COMMENTS_USERS foreign key (USER_ID) references AspNetUsers(ID)
);

drop table if exists ISSUE_COMMENTS;
create table ISSUE_COMMENTS (
	[ID] int IDENTITY(1,1) not null,
	[TEXT] text null,
	[USER_ID] nvarchar(450) not null,
	[ISSUE_ID] int not null,
	[DATE_CREATED] datetime default CURRENT_TIMESTAMP,
	[LAST_CHANGE_DATE] datetime default null,
	constraint PK_ISSUE_COMMENTS primary key(ID),
	constraint FK_ISSUE_COMMENTS_TASKS foreign key (ISSUE_ID) references ISSUES(ID),
	constraint FK_ISSUE_COMMENTS_USERS foreign key (USER_ID) references AspNetUsers(ID)
);

drop table if exists TAGS;
create table TAGS(
	[ID] int identity(1,1) not null,
	[VALUE] nvarchar(200) not null,
	[DESCRIPTION] nvarchar(500) not null,
	constraint PK_TAGS primary key(ID)
);

drop table if exists TASK_TAGS;
create table TASK_TAGS (
	[ID] int IDENTITY(1,1) not null,
	[TAG_ID] int not null,
	[TASK_ID] int null,
	constraint PK_TASK_TAGS primary key(ID),
	constraint FK_TASK_TAGS_TASKS foreign key (TASK_ID) references TASKS(ID),
	constraint FK_TASK_TAGS_TAGS foreign key (TAG_ID) references TAGS(ID)
);

insert into task_manager.dbo.STATUS (TAG, VALUE) values ('ACTIVE', 'Active'), ('INACTIVE', 'Inactive'), ('SUSPENDED', 'Suspended');

insert into task_manager.dbo.PROJECT_ROLES (ROLE_NAME, ROLE_DESCRIPTION) values ('ADMIN','Administrator'), ('MOD','Moderator'), ('USER','Regular User');

insert into task_manager.dbo.CATEGORIES (VALUE , DESCRIPTION) values ('TECH','Technology'), ('MANAGEMENT','Management'), ('AIRLINE','Airline');

insert into task_manager.dbo.TAGS (VALUE ,  DESCRIPTION) values ('FUN','Very FUN Task'), ('EZ','Easy'), ('HARD','It''s hard man');

insert into task_manager.dbo.STATES (TAG, VALUE) values ('TODO', 'To Do'), ('IN PROGRESS', 'In Progress'), ('CHECKING', 'Checking'), ('IN INTEGRATION', 'In Integration'), ('DONE', 'Done');

insert into task_manager.dbo.PRIORITIES (TAG, VALUE) values ('LOW', 'Low Priority'), ('MEDIUM', 'Medium Priority'), ('HIGH PRIORITY', 'High Priority');

/* dodati sličice,
trello
charts
*/