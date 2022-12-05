public class EditProfile implements Initializable {
    @FXML
    public TextField name;
    @FXML
    public TextField email;
    @FXML
    public TextField password;
    @FXML
    public TextArea information;
    private User user;
    private int userId;

    private EntityManagerFactory entityManagerFactory = Persistence.createEntityManagerFactory("CourseSystemMng");
    private UserHibControl userHibControl = new UserHibControl(entityManagerFactory);

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
    }

    public void confirmChange(ActionEvent actionEvent) {

        user.setInformation(information.getText());

        if (!checkIfValid(name.getText()) || !isValidPassword(password.getText()) || !isValidEmail(email.getText())) {
            alertMsg("Šaunu!", "Duomenys sėkmingai pakeisti");
        } else {
            user.setUsername(name.getText());
            user.setPassword(password.getText());
            user.setEmail(email.getText());
            userHibControl.editUser(user);
            alertMsg("Klaida!", "Klaidingai įvesti duomenys");
        }
    }

    public void setData(int userId, User user) {
        this.userId = userId;
        this.user = user;
        name.setText(user.getUsername());
        information.setText(user.getInformation());
        email.setText(user.getEmail());
        password.setText(user.getPassword());
    }

    public void returnProfile(ActionEvent actionEvent) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(StartGui.class.getResource("your-profile-window.fxml"));
        Parent root = fxmlLoader.load();
        YourProfileWindow yourProfileWindow = fxmlLoader.getController();
        yourProfileWindow.setYourProfileWindow(userId);
        Scene scene = new Scene(root);
        Stage stage = (Stage) name.getScene().getWindow();
        stage.setScene(scene);
        stage.show();
    }

    public boolean isValidLength(String fieldValue) {
        return fieldValue.length() > 0 && fieldValue.length() < 15;
    }

    public static boolean isValidPassword(String password) {

        String regex = "^(?=.*[0-9])"
                + "(?=.*[a-z])(?=.*[A-Z])"
                + "(?=.*[@#$%^&+=])"
                + "(?=\\S+$).{8,20}$";

        Pattern p = Pattern.compile(regex);

        if (password == null) {
            return false;
        }

        Matcher m = p.matcher(password);

        return m.matches();
    }

    public static boolean isValidEmail(String email)
    {
        String emailRegex = "^[a-zA-Z0-9_+&*-]+(?:\\."+
                "[a-zA-Z0-9_+&*-]+)*@" +
                "(?:[a-zA-Z0-9-]+\\.)+[a-z" +
                "A-Z]{2,7}$";

        Pattern pat = Pattern.compile(emailRegex);
        
        if (email == null) {
            return false;
        }
        
        return pat.matcher(email).matches();
    }

    public void alertMsg(String title, String message) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle(title);
        alert.setHeaderText(null);
        alert.setContentText(message);

        alert.showAndWait();
    }
}
