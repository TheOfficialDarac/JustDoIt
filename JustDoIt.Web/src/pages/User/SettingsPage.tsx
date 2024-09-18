import DisplaySettings from "../../components/settings/DisplaySettings";
import UserSettings from "../../components/settings/UserSettings";

const SettingsPage = () => {
  return (
    <div className="m-3 p-3 flex flex-col gap-2">
      <DisplaySettings />
      <UserSettings />
    </div>
  );
};

export default SettingsPage;
