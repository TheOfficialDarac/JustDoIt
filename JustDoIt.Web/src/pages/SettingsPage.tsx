import { Button } from "@nextui-org/react";
import { usePreferences } from "../hooks/usePreferences";

const SettingsPage = () => {
  const { changeTheme } = usePreferences();

  const toggleTheme = () => {
    changeTheme();
  };

  return (
    <div className="m-3 p-3">
      <p></p>
      <Button onClick={toggleTheme}>Change theme</Button>
    </div>
  );
};

export default SettingsPage;
